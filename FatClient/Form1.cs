using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FatClient
{
    public partial class Form1 : Form
    {
        public static string logPath = System.Windows.Forms.Application.StartupPath + @"\Log\";
        public static string EquipmentPath = System.Windows.Forms.Application.StartupPath + @"\Equipment\";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(logPath) == false)
            {
                Directory.CreateDirectory(logPath);
            }
            if (Directory.Exists(EquipmentPath) == false)
            {
                Directory.CreateDirectory(EquipmentPath);
            }
            FileInfo[] fileInfos = getEquipmentPathList();
            foreach(FileInfo fi in fileInfos)
            {
                Equipment equipment = new Equipment(fi);
                equipment.Name = Path.GetFileNameWithoutExtension(fi.FullName);
                addEquipment(equipment);
            }
        }
        private void addEquipment(Equipment equipment)
        {
            equipment.Dock = System.Windows.Forms.DockStyle.Fill;
            equipment.Location = new System.Drawing.Point(3, 3);
            equipment.Size = new System.Drawing.Size(1504, 619);
            equipment.TabIndex = 0;
            System.Windows.Forms.TabPage tabPage1 = new System.Windows.Forms.TabPage();
            tabPage1.AutoScroll = true; // 스크롤 활성화
            tabPage1.SuspendLayout();
            Maintab.Controls.Add(tabPage1);
            tabPage1.Controls.Add(equipment);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = equipment.Name;
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(1510, 625);
            tabPage1.TabIndex = 0;
            tabPage1.Text = equipment.Name;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.ResumeLayout(false);
        }
        public FileInfo[] getEquipmentPathList()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(EquipmentPath);
            return directoryInfo.GetFiles("*.txt");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            String name = string.IsNullOrEmpty(AddClientNameTextBox.Text) ?  (Maintab.Controls.Count + 1).ToString() : AddClientNameTextBox.Text;
            Equipment equipment = new Equipment(name);
            equipment.Name = name;
            addEquipment(equipment);
        }

        private void WebsocketButton_Click(object sender, EventArgs e)
        {
            WebsocketClientForm websocketClientForm = new WebsocketClientForm();
            websocketClientForm.ShowDialog();
        }

        private void LogFolderButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(logPath);
        }
    }
}
