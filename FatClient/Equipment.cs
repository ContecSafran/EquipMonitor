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
        public Equipment(String name)
        {
            InitializeComponent();
            equipment.info.name = name;
            equipment.ResponseTextBox = this.ResponseTextBox;
        }
        public Equipment(FileInfo fi)
        {
            InitializeComponent();
            ReadEquipmentInfo(fi);
            equipment.ResponseTextBox = this.ResponseTextBox;
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
                TcpEquipmentClient client = new TcpEquipmentClient();
                await client.RunAsync(equipment);
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
                equipment.ReceiveResponse("error connect infomation file");
                writer.Close();
                return;
            }
            this.equipment.info.isHex = this.isHexMassage.Checked;
            this.equipment.info.command = this.MessageTextBox.Text;
            this.equipment.info.clientType = this.tcpRadio.Checked ? constants.ClientType.TCP : constants.ClientType.UDP;
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
            this.MessageTextBox.Text = this.equipment.info.command;
        }
    }
}
