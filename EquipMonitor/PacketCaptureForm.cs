using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EquipMonitor
{
    public partial class PacketCaptureForm : Form
    {
        // Raw Socket
        private Socket _socketOut;   // SIO_RCVALL 적용 (주 캡처)
        private Socket _socketIn;    // SIO_RCVALL 미적용 (수신 보조)
        private CancellationTokenSource _cts;

        // 패킷 저장
        private int _packetNo = 0;
        private volatile string[] _filterTerms = new string[0];
        private const int MaxPackets = 10000;
        private readonly List<PacketRecord> _allPackets = new List<PacketRecord>();
        private readonly object _packetsLock = new object();

        // IP-ID 중복 제거 (두 소켓 중복 방지)
        private readonly HashSet<string> _seenIds = new HashSet<string>();
        private readonly Queue<string> _seenQueue = new Queue<string>();
        private const int MaxSeenIds = 2000;
        private readonly object _seenLock = new object();

        // 로컬 IP 목록 (송신/수신 방향 판단용)
        private static readonly HashSet<string> _localIPs = GetLocalIPs();

        private bool IsCapturing => _cts != null && !_cts.IsCancellationRequested;

        public PacketCaptureForm()
        {
            InitializeComponent();
            EnableDoubleBuffer();
            LoadNics();
            filterTextBox.TextChanged += FilterTextBox_TextChanged;
        }

        private static HashSet<string> GetLocalIPs()
        {
            var ips = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            try
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                    foreach (var addr in nic.GetIPProperties().UnicastAddresses)
                        if (addr.Address.AddressFamily == AddressFamily.InterNetwork)
                            ips.Add(addr.Address.ToString());
            }
            catch { }
            return ips;
        }

        private void LoadNics()
        {
            nicComboBox.Items.Clear();
            try
            {
                foreach (var nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus != OperationalStatus.Up) continue;
                    foreach (var addr in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (addr.Address.AddressFamily == AddressFamily.InterNetwork &&
                            !IPAddress.IsLoopback(addr.Address))
                        {
                            nicComboBox.Items.Add(new NicItem(addr.Address.ToString(),
                                $"{nic.Name}  [{addr.Address}]"));
                        }
                    }
                }
            }
            catch { }
            if (nicComboBox.Items.Count > 0)
                nicComboBox.SelectedIndex = 0;
        }

        private void EnableDoubleBuffer()
        {
            typeof(ListView)
                .GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance)
                ?.SetValue(packetListView, true);
        }

        private void FilterTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = filterTextBox.Text.Trim();
            _filterTerms = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (!IsCapturing)
                ApplyFilterToAllPackets();
        }

        private void ApplyFilterToAllPackets()
        {
            List<PacketRecord> snapshot;
            lock (_packetsLock) snapshot = new List<PacketRecord>(_allPackets);
            string[] terms = _filterTerms;
            packetListView.BeginUpdate();
            packetListView.Items.Clear();
            foreach (var pkt in snapshot)
                if (MatchesFilter(pkt, terms))
                    packetListView.Items.Add(CreateListViewItem(pkt));
            packetListView.EndUpdate();
            statusLabel.Text = $"중지됨 — 전체 {snapshot.Count}개 중 {packetListView.Items.Count}개 표시";
        }

        private bool MatchesFilter(PacketRecord pkt, string[] terms)
        {
            if (terms.Length == 0) return true;
            foreach (string term in terms)
            {
                bool hit = pkt.Src.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           pkt.Dst.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           pkt.Proto.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0 ||
                           pkt.Info.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
                if (!hit) return false;
            }
            return true;
        }

        private ListViewItem CreateListViewItem(PacketRecord pkt)
        {
            var item = new ListViewItem(pkt.No.ToString());
            item.SubItems.Add(pkt.Time);
            item.SubItems.Add(pkt.Proto);
            item.SubItems.Add(pkt.Src);
            item.SubItems.Add(pkt.Dst);
            item.SubItems.Add(pkt.Info);
            item.Tag = pkt.HexDump;
            item.ForeColor = pkt.Proto.Contains("↑")
                ? System.Drawing.Color.DarkBlue
                : System.Drawing.Color.DarkGreen;
            return item;
        }

        // ── 시작 / 중지 ──────────────────────────────────────────────────────

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (IsCapturing) StopCapture();
            else StartCapture();
        }

        private void StartCapture()
        {
            if (nicComboBox.SelectedItem == null)
            {
                MessageBox.Show("NIC을 선택해 주세요.", "경고");
                return;
            }
            var nic = (NicItem)nicComboBox.SelectedItem;

            _packetNo = 0;
            lock (_packetsLock) _allPackets.Clear();
            lock (_seenLock) { _seenIds.Clear(); _seenQueue.Clear(); }
            packetListView.Items.Clear();
            detailTextBox.Clear();

            _cts = new CancellationTokenSource();
            bool started = false;

            try
            {
                // 소켓1: SIO_RCVALL (promisc — 송신 캡처 확실, 수신도 시도)
                _socketOut = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                _socketOut.Bind(new IPEndPoint(IPAddress.Parse(nic.IP), 0));
                _socketOut.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                _socketOut.ReceiveBufferSize = 512 * 1024;
                _socketOut.IOControl(IOControlCode.ReceiveAll, new byte[] { 1, 0, 0, 0 }, new byte[] { 0, 0, 0, 0 });

                var token = _cts.Token;
                var t1 = new Thread(() => CaptureLoop(_socketOut, token));
                t1.IsBackground = true;
                t1.Start();
                started = true;

                // 소켓2: SIO_RCVALL 없이 — Windows에서 수신 패킷 도달 시 캡처 시도
                try
                {
                    _socketIn = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                    _socketIn.Bind(new IPEndPoint(IPAddress.Parse(nic.IP), 0));
                    _socketIn.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                    _socketIn.ReceiveBufferSize = 512 * 1024;

                    var t2 = new Thread(() => CaptureLoop(_socketIn, token));
                    t2.IsBackground = true;
                    t2.Start();
                }
                catch
                {
                    // 소켓2 실패는 무시 — 소켓1만으로 계속
                    _socketIn = null;
                }

                startStopButton.Text = "■ 중지";
                nicComboBox.Enabled = false;
                statusLabel.Text = $"캡처 중... ({nic.IP})  [관리자 권한 필요 / Windows Raw Socket 한계로 수신은 제한될 수 있음]";
            }
            catch (Exception ex)
            {
                _cts?.Cancel();
                _cts = null;
                try { _socketOut?.Close(); } catch { }
                _socketOut = null;
                _socketIn = null;
                if (!started)
                    MessageBox.Show($"Raw Socket 생성 실패:\n{ex.Message}\n\n관리자 권한으로 실행해 주세요.", "오류");
            }
        }

        private void StopCapture()
        {
            _cts?.Cancel();

            var so = _socketOut;
            var si = _socketIn;
            _socketOut = null;
            _socketIn = null;
            _cts = null;

            try { so?.Close(); } catch { }
            try { si?.Close(); } catch { }

            startStopButton.Text = "▶ 시작";
            nicComboBox.Enabled = true;

            int total;
            lock (_packetsLock) total = _allPackets.Count;
            statusLabel.Text = $"중지됨 — 전체 {total}개 중 {packetListView.Items.Count}개 표시";
            ApplyFilterToAllPackets();
        }

        // ── 캡처 루프 ────────────────────────────────────────────────────────

        private void CaptureLoop(Socket socket, CancellationToken ct)
        {
            byte[] buf = new byte[65536];
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    int bytes = socket.Receive(buf);
                    if (bytes < 20 || ct.IsCancellationRequested) continue;

                    byte[] pkt = new byte[bytes];
                    Buffer.BlockCopy(buf, 0, pkt, 0, bytes);

                    // IP-ID + 프로토콜 + 주소 조합으로 중복 제거
                    string key = $"{pkt[4]:X2}{pkt[5]:X2}-{pkt[9]:X2}" +
                                 $"-{pkt[12]}.{pkt[13]}.{pkt[14]}.{pkt[15]}" +
                                 $"-{pkt[16]}.{pkt[17]}.{pkt[18]}.{pkt[19]}";
                    lock (_seenLock)
                    {
                        if (_seenIds.Contains(key)) continue;
                        _seenIds.Add(key);
                        _seenQueue.Enqueue(key);
                        if (_seenQueue.Count > MaxSeenIds)
                            _seenIds.Remove(_seenQueue.Dequeue());
                    }

                    ProcessPacket(pkt);
                }
                catch (SocketException se) when (
                    se.SocketErrorCode == SocketError.Interrupted ||
                    se.SocketErrorCode == SocketError.OperationAborted ||
                    se.SocketErrorCode == SocketError.Shutdown)
                {
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    SetStatus($"캡처 오류: {ex.Message}");
                }
            }
        }

        // ── 패킷 파싱 ────────────────────────────────────────────────────────

        private void ProcessPacket(byte[] pkt)
        {
            try
            {
                int ihl = (pkt[0] & 0x0F) * 4;
                if (ihl < 20 || pkt.Length < ihl) return;

                byte proto = pkt[9];
                string srcIP = $"{pkt[12]}.{pkt[13]}.{pkt[14]}.{pkt[15]}";
                string dstIP = $"{pkt[16]}.{pkt[17]}.{pkt[18]}.{pkt[19]}";
                bool outgoing = _localIPs.Contains(srcIP);
                string arrow = outgoing ? "↑" : "↓";

                string protoName, src, dst, info, hexDump;

                if (proto == 6) // TCP
                {
                    if (pkt.Length < ihl + 20) return;
                    int srcPort = pkt[ihl] * 256 + pkt[ihl + 1];
                    int dstPort = pkt[ihl + 2] * 256 + pkt[ihl + 3];
                    byte flags = pkt[ihl + 13];
                    int dataOff = (pkt[ihl + 12] >> 4) * 4;
                    int payloadStart = ihl + dataOff;
                    int payloadLen = pkt.Length - payloadStart;

                    string flagStr = BuildFlagStr(flags);
                    src = $"{srcIP}:{srcPort}";
                    dst = $"{dstIP}:{dstPort}";
                    protoName = $"TCP {arrow}";

                    if (payloadLen > 0 && payloadStart <= pkt.Length)
                    {
                        byte[] payload = new byte[payloadLen];
                        Buffer.BlockCopy(pkt, payloadStart, payload, 0, payloadLen);
                        string extra = TryParseHttp(payload) ?? TryParseWebSocket(payload) ?? BuildRawPreview(payload);
                        info = $"[{flagStr}] {extra}";
                        hexDump = $"TCP {arrow}  {src} → {dst}\nFlags: {flagStr}  Payload: {payloadLen} bytes\n\n" +
                                  BuildHexDump(payload);
                    }
                    else
                    {
                        info = $"[{flagStr}]";
                        hexDump = $"TCP {arrow}  {src} → {dst}\nFlags: {flagStr}  (페이로드 없음)";
                    }
                }
                else if (proto == 17) // UDP
                {
                    if (pkt.Length < ihl + 8) return;
                    int srcPort = pkt[ihl] * 256 + pkt[ihl + 1];
                    int dstPort = pkt[ihl + 2] * 256 + pkt[ihl + 3];
                    int udpPayloadLen = pkt.Length - ihl - 8;
                    src = $"{srcIP}:{srcPort}";
                    dst = $"{dstIP}:{dstPort}";
                    protoName = $"UDP {arrow}";
                    bool isDns = srcPort == 53 || dstPort == 53;
                    info = isDns ? $"DNS  Len={udpPayloadLen}" : $"Len={udpPayloadLen}";
                    hexDump = info;
                    if (udpPayloadLen > 0)
                    {
                        byte[] udpPayload = new byte[udpPayloadLen];
                        Buffer.BlockCopy(pkt, ihl + 8, udpPayload, 0, udpPayloadLen);
                        hexDump = BuildHexDump(udpPayload);
                    }
                }
                else if (proto == 1) // ICMP
                {
                    if (pkt.Length < ihl + 4) return;
                    byte icmpType = pkt[ihl];
                    string icmpDesc = icmpType == 0 ? "Echo Reply"
                                   : icmpType == 8 ? "Echo Request"
                                   : $"Type={icmpType}";
                    src = srcIP; dst = dstIP;
                    protoName = $"ICMP {arrow}";
                    info = icmpDesc;
                    hexDump = info;
                }
                else
                {
                    src = srcIP; dst = dstIP;
                    protoName = $"IP({proto}) {arrow}";
                    info = $"Protocol={proto}";
                    hexDump = info;
                }

                int no = Interlocked.Increment(ref _packetNo);
                string time = DateTime.Now.ToString("HH:mm:ss.fff");
                var record = new PacketRecord
                {
                    No = no, Time = time, Proto = protoName,
                    Src = src, Dst = dst, Info = info, HexDump = hexDump
                };

                lock (_packetsLock)
                {
                    if (_allPackets.Count >= MaxPackets) _allPackets.RemoveAt(0);
                    _allPackets.Add(record);
                }

                if (MatchesFilter(record, _filterTerms) && IsHandleCreated)
                    BeginInvoke(new Action(() => AddRowToView(record, no)));
            }
            catch { }
        }

        private string BuildFlagStr(byte flags)
        {
            var sb = new StringBuilder();
            if ((flags & 0x02) != 0) sb.Append("SYN ");
            if ((flags & 0x10) != 0) sb.Append("ACK ");
            if ((flags & 0x08) != 0) sb.Append("PSH ");
            if ((flags & 0x01) != 0) sb.Append("FIN ");
            if ((flags & 0x04) != 0) sb.Append("RST ");
            return sb.Length > 0 ? sb.ToString().TrimEnd() : "NONE";
        }

        private string BuildRawPreview(byte[] payload)
        {
            int prev = Math.Min(payload.Length, 16);
            var hex = new StringBuilder();
            var ascii = new StringBuilder();
            for (int i = 0; i < prev; i++)
            {
                hex.Append($"{payload[i]:X2} ");
                char c = (char)payload[i];
                ascii.Append(c >= 32 && c < 127 ? c : '.');
            }
            string ell = payload.Length > 16 ? "..." : "";
            return $"Len={payload.Length} [{hex.ToString().TrimEnd()}{ell}] {ascii}{ell}";
        }

        private string BuildHexDump(byte[] data)
        {
            int maxLen = Math.Min(data.Length, 512);
            var sb = new StringBuilder();
            for (int i = 0; i < maxLen; i += 16)
            {
                sb.Append($"{i:X4}  ");
                for (int j = 0; j < 16; j++)
                {
                    if (i + j < maxLen) sb.Append($"{data[i + j]:X2} ");
                    else sb.Append("   ");
                    if (j == 7) sb.Append(" ");
                }
                sb.Append(" | ");
                for (int j = 0; j < 16 && i + j < maxLen; j++)
                {
                    char c = (char)data[i + j];
                    sb.Append(c >= 32 && c < 127 ? c : '.');
                }
                sb.AppendLine();
            }
            if (data.Length > 512)
                sb.AppendLine($"... ({data.Length - 512}바이트 생략, 총 {data.Length}바이트)");
            return sb.ToString();
        }

        private string TryParseHttp(byte[] payload)
        {
            try
            {
                string text = Encoding.UTF8.GetString(payload);
                string[] methods = { "GET ", "POST ", "PUT ", "DELETE ", "HTTP/", "HEAD ", "OPTIONS ", "PATCH " };
                foreach (string m in methods)
                    if (text.StartsWith(m))
                        return text.Split('\n')[0].TrimEnd('\r');
            }
            catch { }
            return null;
        }

        private string TryParseWebSocket(byte[] payload)
        {
            try
            {
                if (payload.Length >= 2)
                {
                    int opcode = payload[0] & 0x0F;
                    bool masked = (payload[1] & 0x80) != 0;
                    int payloadLen = payload[1] & 0x7F;
                    if (opcode == 1 && !masked && payloadLen < 126 && payload.Length >= 2 + payloadLen)
                    {
                        byte[] msgBytes = new byte[payloadLen];
                        Array.Copy(payload, 2, msgBytes, 0, payloadLen);
                        return $"WS Text: {Encoding.UTF8.GetString(msgBytes)}";
                    }
                    if (opcode == 8) return "WS Close";
                    if (opcode == 9) return "WS Ping";
                    if (opcode == 10) return "WS Pong";
                }
            }
            catch { }
            return null;
        }

        private void AddRowToView(PacketRecord pkt, int totalNo)
        {
            if (packetListView.Items.Count >= MaxPackets)
                packetListView.Items.RemoveAt(0);
            var item = CreateListViewItem(pkt);
            packetListView.Items.Add(item);
            if (autoScrollButton.Checked) item.EnsureVisible();
            statusLabel.Text = $"캡처 중... (총 {totalNo}개)";
        }

        private void SetStatus(string msg)
        {
            if (IsHandleCreated)
                BeginInvoke(new Action(() => statusLabel.Text = msg));
        }

        // ── UI 이벤트 ────────────────────────────────────────────────────────

        private void packetListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (packetListView.SelectedItems.Count == 0) { detailTextBox.Clear(); return; }
            detailTextBox.Text = packetListView.SelectedItems[0].Tag as string ?? "";
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            lock (_packetsLock) _allPackets.Clear();
            lock (_seenLock) { _seenIds.Clear(); _seenQueue.Clear(); }
            packetListView.Items.Clear();
            detailTextBox.Clear();
            _packetNo = 0;
            statusLabel.Text = "초기화됨";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cts?.Cancel();
            try { _socketOut?.Close(); } catch { }
            try { _socketIn?.Close(); } catch { }
        }

        // ── 내부 클래스 ──────────────────────────────────────────────────────

        private class PacketRecord
        {
            public int No;
            public string Time, Proto, Src, Dst, Info, HexDump;
        }

        private class NicItem
        {
            public string IP, Display;
            public NicItem(string ip, string display) { IP = ip; Display = display; }
            public override string ToString() => Display;
        }
    }
}
