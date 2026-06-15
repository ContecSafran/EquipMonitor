using EquipMonitor.dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquipMonitor
{
    public partial class Equipment : UserControl
    {
        EquipmentDto equipment = new EquipmentDto();
        TcpEquipmentClient tcpClient = new TcpEquipmentClient();
        private System.Windows.Forms.Timer saveTimer;
        private bool isFirstLayout = true;
        private bool isRestoring = false;
        public Equipment()
        {
            InitializeComponent();
            this.commandInputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            InitTimer();
            equipment.info.name = "1";
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
            InitConnectionCallback();
            InitHexUtil();
            SetDefaultRadioButtons();
        }
        public Equipment(String name)
        {
            InitializeComponent();
            this.commandInputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            InitTimer();
            equipment.info.name = name;
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
            InitConnectionCallback();
            InitHexUtil();
            SetDefaultRadioButtons();
        }
        public Equipment(FileInfo fi)
        {
            InitializeComponent();
            this.commandInputSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            InitTimer();
            ReadEquipmentInfo(fi);
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
            InitConnectionCallback();
            InitHexUtil();
        }

        private void SetDefaultRadioButtons()
        {
            this.tcpRadio.Checked = true;
            this.isHexMassage.Checked = true;
            equipment.info.clientType = constants.ClientType.TCP;
            equipment.info.isHex = true;
        }

        private void InitHexUtil()
        {
            this.hexUtil1.TargetTextBox = this.MessageTextBox;
            this.hexUtil1.Visible = this.isHexMassage.Checked;
            this.isHexMassage.CheckedChanged += (s, e) =>
            {
                this.hexUtil1.Visible = this.isHexMassage.Checked;
            };
        }

        private void InitTimer()
        {
            if (this.components == null)
            {
                this.components = new System.ComponentModel.Container();
            }
            this.saveTimer = new System.Windows.Forms.Timer(this.components);
            this.saveTimer.Interval = 500; // 500ms debounce
            this.saveTimer.Tick += (s, ev) =>
            {
                this.saveTimer.Stop();
                LayoutSettingsManager.Save();
            };
        }

        private void InitConnectionCallback()
        {
            equipment.OnConnectionStateChanged = (connected) =>
            {
                if (this.InvokeRequired)
                {
                    this.Invoke(new Action(() => UpdateStatusUI(connected)));
                }
                else
                {
                    UpdateStatusUI(connected);
                }
            };
            UpdateStatusUI(false);
        }

        private void UpdateStatusUI(bool connected)
        {
            if (connected)
            {
                this.IPLabel.Text = "IP (연결됨)";
                this.IPLabel.ForeColor = System.Drawing.Color.Green;
                this.connectButton.BackColor = System.Drawing.Color.LightGreen;
                this.connectButton.Text = "Connected";
            }
            else
            {
                this.IPLabel.Text = "IP (연결 안됨)";
                this.IPLabel.ForeColor = System.Drawing.Color.Red;
                this.connectButton.BackColor = System.Drawing.SystemColors.Control;
                this.connectButton.Text = "Connect";
            }
        }

        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            WriteEquipmentInfo();
            await Send();
        }

        private void clearResponseButton_Click(object sender, EventArgs e)
        {
            this.ResponseHexTextBox.Clear();
            this.ResponseAsciiTextBox.Clear();
        }

        public void RenameEquipment(string newName)
        {
            string oldFilePath = MainForm.EquipmentPath + equipment.info.name + ".txt";
            string newFilePath = MainForm.EquipmentPath + newName + ".txt";

            if (File.Exists(oldFilePath))
            {
                File.Move(oldFilePath, newFilePath);
            }

            equipment.info.name = newName;
            this.Name = newName;
            WriteEquipmentInfo();
        }

        public void SaveEquipmentInfo()
        {
            WriteEquipmentInfo();
        }

        private void copyCommandButton_Click(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex == -1) return;

            CommandInfo source;
            if (commandListBox.SelectedItem is CommandInfo cmdInfo)
            {
                source = cmdInfo;
            }
            else if (commandListBox.SelectedItem != null)
            {
                source = new CommandInfo { Title = string.Empty, Content = commandListBox.SelectedItem.ToString() };
            }
            else return;

            string newTitle = GenerateUniqueCommandTitle(source.Title);
            var newCmd = new CommandInfo { Title = newTitle, Description = source.Description, Content = source.Content };
            commandListBox.Items.Add(newCmd);
            commandListBox.SelectedItem = newCmd;
            WriteEquipmentInfo();
        }

        private string GenerateUniqueCommandTitle(string sourceTitle)
        {
            var match = System.Text.RegularExpressions.Regex.Match(sourceTitle, @"^(.*)\((\d+)\)$");
            string baseTitle = match.Success ? match.Groups[1].Value : sourceTitle;
            int i = match.Success ? int.Parse(match.Groups[2].Value) + 1 : 1;

            string candidate;
            do
            {
                candidate = $"{baseTitle}({i++})";
            } while (commandListBox.Items.Cast<object>().Any(item =>
                item is CommandInfo cmd ? cmd.Title == candidate : item?.ToString() == candidate));
            return candidate;
        }
        async Task Send()
        {
            if (string.IsNullOrWhiteSpace(equipment.info.ip))
            {
                equipment.ReceiveAsciiResponse("오류: IP 주소를 입력하세요.");
                return;
            }
            if (equipment.info.port <= 0)
            {
                equipment.ReceiveAsciiResponse("오류: 유효한 포트 번호를 입력하세요.");
                return;
            }

            if(equipment.info.clientType == constants.ClientType.TCP)
            {
                await tcpClient.RunAsync(equipment);
            }
            else
            {
                UdpEquipmentClient client = new UdpEquipmentClient();
                await client.RunAsync(equipment);
            }
        }
        void WriteEquipmentInfo()
        {
            StreamWriter writer = File.CreateText(MainForm.EquipmentPath + this.equipment.info.name + ".txt");
            
            this.equipment.info.ip = this.ipText.Text;
            int port = 0;
            Int32.TryParse(this.portTextBox.Text, out port);
            this.equipment.info.port = port;
            this.equipment.info.isHex = this.isHexMassage.Checked;
            
            this.equipment.info.commands.Clear();
            foreach (var item in this.commandListBox.Items)
            {
                if (item is CommandInfo cmdInfo)
                {
                    this.equipment.info.commands.Add(cmdInfo);
                }
                else if (item != null)
                {
                    this.equipment.info.commands.Add(new CommandInfo { Title = string.Empty, Content = item.ToString() });
                }
            }

            this.equipment.info.command = this.MessageTextBox.Text;
            this.equipment.info.clientType = this.tcpRadio.Checked ? constants.ClientType.TCP : constants.ClientType.UDP;
            this.equipment.info.tail = this.tailTextBox.Text;
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(equipment.info, options);
            writer.Write(jsonString);
            writer.Close();
        }
        void ReadEquipmentInfo(FileInfo fi)
        {
            // 2. 파일 내용 읽기
            string jsonString = File.ReadAllText(fi.FullName);

            // 3. 역직렬화 (JSON -> 객체)
            // <T> 부분에 복원할 클래스 명을 넣습니다.
            this.equipment.info = System.Text.Json.JsonSerializer.Deserialize<EquipmentInfo>(jsonString);

            this.equipment.info.name = Path.GetFileNameWithoutExtension(fi.FullName);

            this.ipText.Text = this.equipment.info.ip;
            this.portTextBox.Text = this.equipment.info.port.ToString();
            this.tailTextBox.Text = this.equipment.info.tail;
            if (this.equipment.info.isHex)
            {
                this.isHexMassage.Checked = true;
                this.isAsciiMassage.Checked = false;
            }
            else
            {
                this.isHexMassage.Checked = false;
                this.isAsciiMassage.Checked = true;
            }
            
            if (this.equipment.info.clientType == constants.ClientType.TCP)
            {
                this.tcpRadio.Checked = true;
                this.udpRadio.Checked = false;
            }
            else
            {
                this.tcpRadio.Checked = false;
                this.udpRadio.Checked = true;
            }

            this.commandListBox.Items.Clear();
            if (this.equipment.info.commands != null && this.equipment.info.commands.Count > 0)
            {
                foreach (CommandInfo cmd in this.equipment.info.commands)
                {
                    this.commandListBox.Items.Add(cmd);
                }
            }
            else if (!string.IsNullOrEmpty(this.equipment.info.command))
            {
                this.commandListBox.Items.Add(new CommandInfo { Title = string.Empty, Content = this.equipment.info.command });
            }

            this.MessageTextBox.Text = this.equipment.info.command;
        }
        byte[] cumstomData = null;
        private void button1_Click(object sender, EventArgs e)
        {
            cumstomData = SelectAndConvertFile();
        }

        public byte[] SelectAndConvertFile()
        {
            // 1. 파일 선택 창(OpenFileDialog) 생성 및 설정
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\"; // 초기 디렉토리
                openFileDialog.Filter = "모든 파일 (*.*)|*.*"; // 파일 필터
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                // 2. 사용자가 파일을 선택하고 '확인'을 눌렀는지 확인
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 선택한 파일의 경로 가져오기
                    string filePath = openFileDialog.FileName;

                    try
                    {
                        // 3. 파일을 바이너리로 읽어 바이트 배열로 반환
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        return fileBytes;
                    }
                    catch (IOException ex)
                    {
                        // 파일 읽기 실패 시 예외 처리
                        Console.WriteLine($"파일을 읽는 중 오류가 발생했습니다: {ex.Message}");
                        return null;
                    }
                }
            }

            // 파일을 선택하지 않고 취소한 경우
            return null;
        }

        private void button2_ClickAsync(object sender, EventArgs e)
        {
            WriteEquipmentInfo();
            SendBuffer();
        }
        private async void SendBuffer()
        {

            if (cumstomData != null)
            {
                await tcpClient.RunAsyncByBuffer(equipment, cumstomData);
            }
            else
            {
                MessageBox.Show("선택된 데이터가 없습니다");
            }
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            WriteEquipmentInfo();
            if (equipment.info.clientType == constants.ClientType.TCP)
            {
                await tcpClient.ConnectAsync(equipment);
            }
            else
            {
                equipment.ReceiveAsciiResponse("UDP 모드입니다.");
            }
        }

        private async void disconnectButton_Click(object sender, EventArgs e)
        {
            if (equipment.info.clientType == constants.ClientType.TCP)
            {
                await tcpClient.CloseAsync();
                equipment.ReceiveAsciiResponse("TCP 연결이 해제되었습니다.");
            }
        }

        private void commandListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex != -1)
            {
                if (commandListBox.SelectedItem is CommandInfo cmdInfo)
                {
                    this.TitleTextBox.Text = cmdInfo.Title;
                    this.DescriptionTextBox.Text = cmdInfo.Description;
                    this.MessageTextBox.Text = cmdInfo.Content;
                }
                else if (commandListBox.SelectedItem != null)
                {
                    this.TitleTextBox.Text = string.Empty;
                    this.DescriptionTextBox.Text = string.Empty;
                    this.MessageTextBox.Text = commandListBox.SelectedItem.ToString();
                }
            }
        }

        private async void commandListBox_DoubleClick(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex == -1) return;

            string content = commandListBox.SelectedItem is CommandInfo cmdInfo
                ? cmdInfo.Content
                : commandListBox.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(content))
            {
                WriteEquipmentInfo();
                await Send();
            }
        }

        private void addCommandButton_Click(object sender, EventArgs e)
        {
            string title = this.TitleTextBox.Text.Trim();
            string description = this.DescriptionTextBox.Text.Trim();
            string content = this.MessageTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(content))
            {
                bool exists = false;
                CommandInfo matchedItem = null;
                foreach (var item in commandListBox.Items)
                {
                    if (item is CommandInfo cmdInfo && cmdInfo.Title == title && cmdInfo.Description == description && cmdInfo.Content == content)
                    {
                        exists = true;
                        matchedItem = cmdInfo;
                        break;
                    }
                }

                if (!exists)
                {
                    var newCmd = new CommandInfo { Title = title, Description = description, Content = content };
                    commandListBox.Items.Add(newCmd);
                    commandListBox.SelectedItem = newCmd;
                    WriteEquipmentInfo();
                }
                else
                {
                    commandListBox.SelectedItem = matchedItem;
                }
            }
        }

        private void deleteCommandButton_Click(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex != -1)
            {
                commandListBox.Items.RemoveAt(commandListBox.SelectedIndex);
                this.TitleTextBox.Clear();
                this.DescriptionTextBox.Clear();
                this.MessageTextBox.Clear();
                WriteEquipmentInfo();
            }
        }

        private void modifyCommandButton_Click(object sender, EventArgs e)
        {
            int selectedIndex = commandListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                string title = this.TitleTextBox.Text.Trim();
                string description = this.DescriptionTextBox.Text.Trim();
                string content = this.MessageTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(content))
                {
                    var updatedCmd = new CommandInfo { Title = title, Description = description, Content = content };
                    commandListBox.Items[selectedIndex] = updatedCmd;
                    commandListBox.SelectedIndex = selectedIndex;
                    WriteEquipmentInfo();
                }
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (isFirstLayout && this.Visible && this.Height > 0)
            {
                if (RestoreSplitterDistance())
                {
                    isFirstLayout = false;
                }
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            if (isFirstLayout && this.Visible && this.Height > 0)
            {
                if (RestoreSplitterDistance())
                {
                    isFirstLayout = false;
                }
            }
        }

        private bool RestoreSplitterDistance()
        {
            if (this.equipment != null && this.equipment.info != null && !string.IsNullOrEmpty(this.equipment.info.name))
            {
                string name = this.equipment.info.name;
                if (LayoutSettingsManager.Settings.EquipmentSplitterDistances.ContainsKey(name))
                {
                    int dist = LayoutSettingsManager.Settings.EquipmentSplitterDistances[name];
                    try
                    {
                        int min = commandInputSplitContainer.Panel1MinSize;
                        int max = commandInputSplitContainer.Height - commandInputSplitContainer.Panel2MinSize;
                        if (dist >= min && dist <= max)
                        {
                            isRestoring = true;
                            commandInputSplitContainer.SplitterDistance = dist;
                            isRestoring = false;
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        isRestoring = false;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        private void commandInputSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (isRestoring) return;

            if (this.equipment != null && this.equipment.info != null && !string.IsNullOrEmpty(this.equipment.info.name))
            {
                LayoutSettingsManager.Settings.EquipmentSplitterDistances[this.equipment.info.name] = commandInputSplitContainer.SplitterDistance;
                if (saveTimer != null)
                {
                    saveTimer.Stop();
                    saveTimer.Start();
                }
            }
        }
    }
}
