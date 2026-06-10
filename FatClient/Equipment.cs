using FatClient.dto;
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

namespace FatClient
{
    public partial class Equipment : UserControl
    {
        EquipmentDto equipment = new EquipmentDto();
        TcpEquipmentClient tcpClient = new TcpEquipmentClient();
        public Equipment()
        {
            InitializeComponent();
            equipment.info.name = "1";
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
        }
        public Equipment(String name)
        {
            InitializeComponent();
            equipment.info.name = name;
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
        }
        public Equipment(FileInfo fi)
        {
            InitializeComponent();
            ReadEquipmentInfo(fi);
            equipment.ResponseHexTextBox = this.ResponseHexTextBox;
            equipment.ResponseAsciiTextBox = this.ResponseAsciiTextBox;
        }

        private async void SendMessageButton_Click(object sender, EventArgs e)
        {
            WriteEquipmentInfo();
            await Send();
        }
        async Task Send()
        {
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
            StreamWriter writer = File.CreateText(Form1.EquipmentPath + this.equipment.info.name + ".txt");
            
            this.equipment.info.ip = this.ipText.Text;
            int port = 0;
            if (Int32.TryParse(this.portTextBox.Text, out port))
            {
                this.equipment.info.port = port;
            }
            else
            {
                equipment.ReceiveAsciiResponse("error connect infomation file");
                writer.Close();
                return;
            }
            this.equipment.info.isHex = this.isHexMassage.Checked;
            
            this.equipment.info.commands.Clear();
            foreach (var item in this.commandListBox.Items)
            {
                this.equipment.info.commands.Add(item.ToString());
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
                foreach (string cmd in this.equipment.info.commands)
                {
                    this.commandListBox.Items.Add(cmd);
                }
            }
            else if (!string.IsNullOrEmpty(this.equipment.info.command))
            {
                this.commandListBox.Items.Add(this.equipment.info.command);
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
                this.MessageTextBox.Text = commandListBox.SelectedItem.ToString();
            }
        }

        private void addCommandButton_Click(object sender, EventArgs e)
        {
            string newCommand = this.MessageTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(newCommand))
            {
                if (!commandListBox.Items.Contains(newCommand))
                {
                    commandListBox.Items.Add(newCommand);
                    commandListBox.SelectedItem = newCommand;
                    WriteEquipmentInfo();
                }
            }
        }

        private void deleteCommandButton_Click(object sender, EventArgs e)
        {
            if (commandListBox.SelectedIndex != -1)
            {
                commandListBox.Items.RemoveAt(commandListBox.SelectedIndex);
                this.MessageTextBox.Clear();
                WriteEquipmentInfo();
            }
        }
    }
}
