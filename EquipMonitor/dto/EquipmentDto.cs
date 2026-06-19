using EquipMonitor.constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EquipMonitor.dto
{
    public class EquipmentDto
    {
        public EquipmentInfo info = new EquipmentInfo();

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Action<bool> OnConnectionStateChanged { get; set; }

        public System.Windows.Forms.TextBoxBase ResponseHexTextBox
        {
            get;
            set;
        }
        public System.Windows.Forms.TextBoxBase ResponseAsciiTextBox
        {
            get;
            set;
        }
        public System.Windows.Forms.TextBoxBase LogTextBox
        {
            get;
            set;
        }
        string logFilePath;
        public void initLogFile()
        {
            logFilePath = MainForm.logPath + string.Format("{0}_{1}.txt", DateTime.Now.ToString("yyyyMMddHHmmss"), this.info.name);
        }
        public void ReceiveResponse(string msg, System.Windows.Forms.TextBoxBase textBox)
        {
            if (textBox != null)
            {
                if (textBox.InvokeRequired)
                {
                    textBox.Invoke((Action)delegate
                    {
                        ReceiveResponse(msg, textBox);
                    });
                }
                else
                {
                    // 단독 \n 문자를 Windows TextBox 호환 개행문자인 \r\n으로 치환
                    string formattedMsg = msg.Replace("\r\n", "\n").Replace("\n", "\r\n");

                    string Time = DateTime.Now.ToString("[yyyy/MM/dd HH:mm:ss]");
                    const int MaxTextLength = 200000;
                    if (textBox.TextLength > MaxTextLength)
                        textBox.Text = textBox.Text.Substring(textBox.TextLength - MaxTextLength / 2);
                    textBox.AppendText("\r\n" + Time + "\t" + formattedMsg);
                    textBox.Select(textBox.Text.Length, 0);
                    textBox.ScrollToCaret();
                    if (!string.IsNullOrEmpty(logFilePath))
                    {
                        using (StreamWriter sw = new StreamWriter(logFilePath, true))
                        {
                            sw.WriteLine(formattedMsg);
                        }
                    }
                }
            }
        }
        public void ReceiveHexResponse(string msg)
        {
            this.ReceiveResponse(msg, ResponseHexTextBox);
        }

        public void ReceiveAsciiResponse(string msg)
        {
            this.ReceiveResponse(msg, ResponseAsciiTextBox);
        }

        public void ReceiveLogResponse(string msg)
        {
            this.ReceiveResponse(msg, LogTextBox);
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public Action<PacketInfo> OnPacketAdded { get; set; }

        public void AddPacket(byte[] data, bool isSend)
        {
            if (data == null || data.Length == 0) return;
            var packet = new PacketInfo { Time = DateTime.Now, IsSend = isSend, Data = data };

            if (!string.IsNullOrEmpty(logFilePath))
            {
                string direction = isSend ? "TX" : "RX";
                string hexStr = BitConverter.ToString(data).Replace("-", " ");
                string asciiStr = Encoding.UTF8.GetString(data);
                string timeStr = packet.Time.ToString("[yyyy/MM/dd HH:mm:ss.fff]");
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"{timeStr} [{direction}] HEX   : {hexStr}");
                    sw.WriteLine($"{timeStr} [{direction}] ASCII : {asciiStr}");
                }
            }

            OnPacketAdded?.Invoke(packet);
        }
    }
}
