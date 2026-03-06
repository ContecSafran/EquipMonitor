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
        public System.Windows.Forms.TextBox ResponseTextBox
        {
            get;
            set;
        }
        string logFilePath;
        public void initLogFile()
        {
            logFilePath = Form1.logPath + string.Format("{0}_{1}.txt", DateTime.Now.ToString("yyyyMMddhhmmss"), this.info.name);
        }
        public void ReceiveResponse(string msg)
        {

            if (ResponseTextBox != null)
            {
                if (ResponseTextBox.InvokeRequired)
                {
                    ResponseTextBox.Invoke((Action)delegate
                    {
                        ReceiveResponse(msg);
                    });
                }
                else
                {
                    Console.WriteLine(msg);
                    ResponseTextBox.Text = ResponseTextBox.Text + "\r\n" + msg;
                    StreamWriter sw = new StreamWriter(logFilePath, true);
                    sw.WriteLine(msg);
                    sw.Close();
                }
            }
        }
    }
}
