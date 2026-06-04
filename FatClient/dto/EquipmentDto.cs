using FatClient.constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FatClient.dto
{
    public class EquipmentDto
    {
        public EquipmentInfo info = new EquipmentInfo();
        public System.Windows.Forms.TextBox ResponseHexTextBox
        {
            get;
            set;
        }
        public System.Windows.Forms.TextBox ResponseAsciiTextBox
        {
            get;
            set;
        }
        string logFilePath;
        public void initLogFile()
        {
            logFilePath = Form1.logPath + string.Format("{0}_{1}.txt", DateTime.Now.ToString("yyyyMMddhhmmss"), this.info.name);
        }
        public void ReceiveResponse(string msg, System.Windows.Forms.TextBox textBox)
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
                    textBox.Text = textBox.Text + "\r\n" + Time + "\t" + formattedMsg;
                    textBox.Select(textBox.Text.Length, 0);
                    textBox.ScrollToCaret();
                    StreamWriter sw = new StreamWriter(logFilePath, true);
                    sw.WriteLine(formattedMsg);
                    sw.Close();
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
    }
}
