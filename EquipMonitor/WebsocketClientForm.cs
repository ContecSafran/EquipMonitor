using EquipMonitor.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMonitor
{
    public partial class WebsocketClientForm : System.Windows.Forms.Form
    {
        private ClientWebSocket _webSocket; // 연결 시마다 새로 생성해야 함
        private CancellationTokenSource _cts;

        private WebsocketInfo websocketInfo = new WebsocketInfo();
        string logFilePath;

        public WebsocketClientForm()
        {
            InitializeComponent();
            ReadEquipmentInfo();
        }

        private async void ConnectButton_Click(object sender, EventArgs e)
        {
            if (_webSocket != null && _webSocket.State == WebSocketState.Open)
            {
                await StopAsync();
                ConnectButton.Text = "접속";
            }
            else
            {
                WriteEquipmentInfo();
                initLogFile();
                await StartAsync();
                // 연결 성공 여부에 따라 버튼 텍스트 결정
                ConnectButton.Text = (_webSocket != null && _webSocket.State == WebSocketState.Open)
                    ? "접속 해제"
                    : "접속";
            }
        }

        void WriteEquipmentInfo()
        {
            this.websocketInfo.url = this.urlTextBox.Text;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this.websocketInfo, options);
            using (StreamWriter writer = File.CreateText(MainForm.EquipmentPath + "websocket.json"))
            {
                writer.Write(jsonString);
            }
        }

        void ReadEquipmentInfo()
        {
            if(!File.Exists(MainForm.EquipmentPath + "websocket.json"))
            {
                return;
            }
            // 2. 파일 내용 읽기
            string jsonString = File.ReadAllText(MainForm.EquipmentPath + "websocket.json");

            // 3. 역직렬화 (JSON -> 객체)
            // <T> 부분에 복원할 클래스 명을 넣습니다.
            this.websocketInfo = System.Text.Json.JsonSerializer.Deserialize<WebsocketInfo>(jsonString);

            this.urlTextBox.Text = this.websocketInfo.url;
            /*
            this.idText.Text = DecodeBasicAuthToken(this.websocketInfo.base64Auth).username;
            this.passwordTextBox.Text = DecodeBasicAuthToken(this.websocketInfo.base64Auth).password;*/
        }

        /// <summary>
        /// Basic Auth 토큰을 입력받아 Username과 Password를 반환합니다.
        /// </summary>
        /// <param name="token">Base64로 인코딩된 토큰 문자열 (예: dXNlcjpwYXNz)</param>
        /// <returns>(Username, Password) 튜플</returns>
        private (string username, string password) DecodeBasicAuthToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return (string.Empty, string.Empty);
            }

            try
            {
                // 1. "Basic " 접두사가 붙어있다면 제거 (실수로 전체 헤더 값을 넣었을 경우 대비)
                if (token.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    token = token.Substring(6);
                }

                // 2. Base64 디코딩 (Base64 String -> Byte[])
                byte[] bytes = Convert.FromBase64String(token);

                // 3. 바이트를 문자열로 변환 (인코딩은 생성할 때와 맞춰야 함, 보통 UTF-8 사용)
                // 기존 코드에서 Encoding.ASCII를 썼다면 여기서도 ASCII를 써야 하지만, 
                // UTF8은 ASCII를 포함하므로 호환성이 좋습니다.
                string credentials = Encoding.UTF8.GetString(bytes);

                // 4. "username:password" 형식에서 첫 번째 ':'를 기준으로 분리
                // 비밀번호 내부에 ':'가 있을 수 있으므로 Split 개수를 2개로 제한해야 함.
                string[] parts = credentials.Split(new char[] { ':' }, 2);

                string user = parts.Length > 0 ? parts[0] : string.Empty;
                string pass = parts.Length > 1 ? parts[1] : string.Empty;

                return (user, pass);
            }
            catch (Exception)
            {
                // 디코딩 실패 시 (Base64 형식이 아니거나 오류 발생)
                return ("Error", "Decoding Failed");
            }
        }
        public async Task StartAsync()
        {
            // ClientWebSocket은 재사용이 불가능하므로 매번 새로 생성
            _webSocket = new ClientWebSocket();
            _cts = new CancellationTokenSource();

            Uri serverUri = new Uri(this.websocketInfo.url);

            if (!string.IsNullOrEmpty(this.websocketInfo.base64Auth))
            {
                _webSocket.Options.SetRequestHeader("Authorization", $"Basic {this.websocketInfo.base64Auth}");
            }

            try
            {
                await _webSocket.ConnectAsync(serverUri, CancellationToken.None);
                ReceiveResponse("서버에 연결되었습니다. [Url : " + this.websocketInfo.url + "]");

                // await SendMessageAsync("ConstellationsOn\n"); // 특수 목적의 하드코딩 제거

                // Task.Run으로 수신 루프 시작
                _ = Task.Run(() => ReceiveDataAsync(_cts.Token));
            }
            catch (Exception ex)
            {
                ReceiveResponse($"연결 오류: {ex.Message}");
            }
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {
            await DoSend();
        }

        private async void sendMessageTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await DoSend();
            }
        }

        private async Task DoSend()
        {
            string message = sendMessageTextBox.Text;
            if (string.IsNullOrEmpty(message)) return;
            await SendMessageAsync(message);
            ReceiveResponse($"[송신] {message}");
            sendMessageTextBox.Clear();
        }

        public async Task SendMessageAsync(string message)
        {
            if (_webSocket == null || _webSocket.State != WebSocketState.Open) return;
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            await _webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task StopAsync()
        {
            try
            {
                if (_webSocket != null)
                {
                    _cts?.Cancel();
                    _cts?.Dispose();
                    _cts = null;

                    if (_webSocket.State == WebSocketState.Open || _webSocket.State == WebSocketState.CloseReceived)
                    {
                        await _webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client stop", CancellationToken.None);
                    }

                    _webSocket.Dispose();
                    _webSocket = null;
                    ReceiveResponse("연결이 안전하게 종료되었습니다.");
                }
            }
            catch (Exception ex)
            {
                ReceiveResponse($"종료 중 오류 발생: {ex.Message}");
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            if (_webSocket != null)
            {
                _webSocket.Dispose();
                _webSocket = null;
            }
        }

        private async Task ReceiveDataAsync(CancellationToken ct)
        {
            byte[] buffer = new byte[1024 * 64]; // 자바 데이터가 크므로 64KB 권장
            try
            {
                StringBuilder messageBuilder = new StringBuilder();
                while (_webSocket != null && _webSocket.State == WebSocketState.Open && !ct.IsCancellationRequested)
                {
                    var result = await _webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), ct);
                    if (result.MessageType == WebSocketMessageType.Close) break;

                    string part = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    messageBuilder.Append(part);

                    if (result.EndOfMessage)
                    {
                        string data = messageBuilder.ToString();
                        // 내용이 너무 길면 자르기 등 추가 가능
                        ReceiveResponse($"[수신] {data}");
                        messageBuilder.Clear();
                    }
                }
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                ReceiveResponse($"수신 중 오류: {ex.Message}");
            }
        }

        public void initLogFile()
        {
            logFilePath = MainForm.logPath + string.Format("Websocket {0}.txt", DateTime.Now.ToString("yyyyMMddhhmmss"));
        }

        private static readonly object _fileLock = new object();

        public void ReceiveResponse(string msg)
        {
            if (ResponseTextBox == null) return;

            if (ResponseTextBox.InvokeRequired)
            {
                ResponseTextBox.Invoke(new Action(() => ReceiveResponse(msg)));
            }
            else
            {
                ResponseTextBox.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}\r\n");

                Task.Run(() =>
                {
                    lock (_fileLock)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(logFilePath, true, Encoding.UTF8))
                            {
                                sw.WriteLine(msg);
                            }
                        }
                        catch { /* 파일 쓰기 실패 무시 */ }
                    }
                });
            }
        }
    }
}
