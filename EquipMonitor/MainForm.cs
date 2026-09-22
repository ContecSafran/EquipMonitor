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

namespace EquipMonitor
{
    public partial class MainForm : Form
    {
        public static string logPath = System.Windows.Forms.Application.StartupPath + @"\Log\";
        public static string EquipmentPath = System.Windows.Forms.Application.StartupPath + @"\Equipment\";
        public MainForm()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            this.ResizeBegin += MainForm_ResizeBegin;
            this.ResizeEnd += MainForm_ResizeEnd;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            this.SuspendLayout();
            foreach (TabPage tab in Maintab.TabPages)
                foreach (Control c in tab.Controls)
                    c.SuspendLayout();
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            foreach (TabPage tab in Maintab.TabPages)
                foreach (Control c in tab.Controls)
                    c.ResumeLayout(true);
            this.ResumeLayout(true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LayoutSettingsManager.Load();
            this.Width = LayoutSettingsManager.Settings.FormWidth;
            this.Height = LayoutSettingsManager.Settings.FormHeight;
            this.WindowState = LayoutSettingsManager.Settings.FormState;

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                LayoutSettingsManager.Settings.FormWidth = this.Width;
                LayoutSettingsManager.Settings.FormHeight = this.Height;
            }
            else
            {
                LayoutSettingsManager.Settings.FormWidth = this.RestoreBounds.Width;
                LayoutSettingsManager.Settings.FormHeight = this.RestoreBounds.Height;
            }
            LayoutSettingsManager.Settings.FormState = this.WindowState;
            LayoutSettingsManager.Save();
        }
        private void addEquipment(Equipment equipment)
        {
            equipment.SetLogTextBox(this.logTextBox);
            equipment.SetHexGroupSize(GetCurrentHexGroupSize());
            System.Windows.Forms.Panel containerPanel = new System.Windows.Forms.Panel();
            containerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            containerPanel.AutoScroll = true; // 스크롤 활성화

            // Dock.Fill + MinimumSize 조합: 패널이 크면 꽉 채우고,
            // 최소 크기보다 작아지면 AutoScroll 이 스크롤바를 표시한다.
            // (Resize 이벤트에 의존하면 첫 탭 추가 시 이벤트가 발생하지 않아 Fill 되지 않음)
            equipment.MinimumSize = new System.Drawing.Size(750, 350); // 최소 보장 크기
            equipment.Location = new System.Drawing.Point(0, 0);
            equipment.Dock = System.Windows.Forms.DockStyle.Fill;
            equipment.TabIndex = 0;

            System.Windows.Forms.TabPage tabPage1 = new System.Windows.Forms.TabPage();
            tabPage1.SuspendLayout();
            Maintab.Controls.Add(tabPage1);
            tabPage1.Controls.Add(containerPanel);
            containerPanel.Controls.Add(equipment);
            tabPage1.Location = new System.Drawing.Point(4, 22);
            tabPage1.Name = equipment.Name;
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.TabIndex = 0;
            tabPage1.Text = equipment.Name;
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.ResumeLayout(true);
            tabPage1.PerformLayout();
        }
        public FileInfo[] getEquipmentPathList()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(EquipmentPath);
            return directoryInfo.GetFiles("*.txt");
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            string name = string.IsNullOrEmpty(ClientNameTextBox.Text)
                ? (Maintab.Controls.Count + 1).ToString()
                : ClientNameTextBox.Text;
            Equipment equipment = new Equipment(name);
            equipment.Name = name;
            addEquipment(equipment);
        }

        private void RenameClientButton_Click(object sender, EventArgs e)
        {
            TabPage selectedTab = Maintab.SelectedTab;
            if (selectedTab == null) return;

            string newName = ClientNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("변경할 이름을 입력하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selectedTab.Name == newName) return;

            foreach (TabPage tab in Maintab.TabPages)
            {
                if (tab != selectedTab && tab.Name == newName)
                {
                    MessageBox.Show($"'{newName}' 이름이 이미 존재합니다.", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Equipment equipment = FindEquipment(selectedTab);
            if (equipment == null) return;

            equipment.RenameEquipment(newName);
            selectedTab.Name = newName;
            selectedTab.Text = newName;
        }

        private Equipment FindEquipment(TabPage tabPage)
        {
            foreach (Control ctrl in tabPage.Controls)
            {
                foreach (Control inner in ctrl.Controls)
                {
                    if (inner is Equipment eq) return eq;
                }
            }
            return null;
        }

        private int GetCurrentHexGroupSize()
        {
            foreach (var btn in new[] { hexBtn4, hexBtn8, hexBtn16, hexBtn32, hexBtn64 })
                if (btn.Checked && int.TryParse(btn.Text, out int v)) return v;
            return 16;
        }

        private void hexBytesPerLine_Click(object sender, EventArgs e)
        {
            var clicked = (ToolStripButton)sender;
            foreach (var btn in new[] { hexBtn4, hexBtn8, hexBtn16, hexBtn32, hexBtn64 })
                btn.Checked = (btn == clicked);

            int groupSize = GetCurrentHexGroupSize();
            foreach (TabPage tab in Maintab.TabPages)
            {
                Equipment eq = FindEquipment(tab);
                eq?.SetHexGroupSize(groupSize);
            }
        }

        private void CopyEquipmentButton_Click(object sender, EventArgs e)
        {
            TabPage selectedTab = Maintab.SelectedTab;
            if (selectedTab == null) return;

            string baseName = selectedTab.Name;
            string newName = GenerateUniqueClientName(baseName);

            string srcFilePath = EquipmentPath + baseName + ".txt";
            string destFilePath = EquipmentPath + newName + ".txt";

            if (File.Exists(srcFilePath))
            {
                File.Copy(srcFilePath, destFilePath);
            }

            Equipment newEquipment;
            FileInfo destFile = new FileInfo(destFilePath);
            if (destFile.Exists)
            {
                newEquipment = new Equipment(destFile);
            }
            else
            {
                newEquipment = new Equipment(newName);
            }
            newEquipment.Name = newName;
            newEquipment.SaveEquipmentInfo();
            addEquipment(newEquipment);
        }

        private string GenerateUniqueClientName(string sourceName)
        {
            var match = System.Text.RegularExpressions.Regex.Match(sourceName, @"^(.*)\((\d+)\)$");
            string baseName = match.Success ? match.Groups[1].Value : sourceName;
            int i = match.Success ? int.Parse(match.Groups[2].Value) + 1 : 1;

            string candidate;
            do
            {
                candidate = $"{baseName}({i++})";
            } while (Maintab.TabPages.Cast<TabPage>().Any(t => t.Name == candidate) ||
                     File.Exists(EquipmentPath + candidate + ".txt"));
            return candidate;
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

        private void DeleteEquipmentButton_Click(object sender, EventArgs e)
        {
            TabPage selectedTab = Maintab.SelectedTab;
            if (selectedTab == null) return;

            string name = selectedTab.Name;
            var result = MessageBox.Show(
                $"'{name}' 장비를 삭제하시겠습니까?",
                "장비 삭제",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            Maintab.Controls.Remove(selectedTab);
            selectedTab.Dispose();

            string filePath = EquipmentPath + name + ".txt";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
