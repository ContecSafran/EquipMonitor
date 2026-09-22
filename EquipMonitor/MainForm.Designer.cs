
namespace EquipMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Maintab = new System.Windows.Forms.TabControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ClientNameLabel = new System.Windows.Forms.ToolStripLabel();
            this.ClientNameTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.RenameClientButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.WebsocketButton = new System.Windows.Forms.ToolStripButton();
            this.LogFolderButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteEquipmentButton = new System.Windows.Forms.ToolStripButton();
            this.CopyEquipmentButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.hexGroupLabel = new System.Windows.Forms.ToolStripLabel();
            this.hexBtn4 = new System.Windows.Forms.ToolStripButton();
            this.hexBtn8 = new System.Windows.Forms.ToolStripButton();
            this.hexBtn16 = new System.Windows.Forms.ToolStripButton();
            this.hexBtn32 = new System.Windows.Forms.ToolStripButton();
            this.hexBtn64 = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Maintab
            // 
            this.Maintab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Maintab.Location = new System.Drawing.Point(0, 0);
            this.Maintab.Name = "Maintab";
            this.Maintab.SelectedIndex = 0;
            this.Maintab.Size = new System.Drawing.Size(822, 290);
            this.Maintab.TabIndex = 5;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Maintab);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.logTextBox);
            this.splitContainer1.Size = new System.Drawing.Size(822, 358);
            this.splitContainer1.SplitterDistance = 290;
            this.splitContainer1.TabIndex = 7;
            // 
            // logTextBox
            // 
            this.logTextBox.BackColor = System.Drawing.Color.Black;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.logTextBox.ForeColor = System.Drawing.Color.LightGreen;
            this.logTextBox.Location = new System.Drawing.Point(0, 0);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(822, 64);
            this.logTextBox.TabIndex = 0;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(822, 358);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(822, 383);
            this.toolStripContainer1.TabIndex = 6;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ClientNameLabel,
            this.ClientNameTextBox,
            this.AddButton,
            this.RenameClientButton,
            this.toolStripSeparator1,
            this.WebsocketButton,
            this.LogFolderButton,
            this.DeleteEquipmentButton,
            this.CopyEquipmentButton,
            this.toolStripSeparator2,
            this.hexGroupLabel,
            this.hexBtn4,
            this.hexBtn8,
            this.hexBtn16,
            this.hexBtn32,
            this.hexBtn64});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(819, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // ClientNameLabel
            // 
            this.ClientNameLabel.Name = "ClientNameLabel";
            this.ClientNameLabel.Size = new System.Drawing.Size(34, 22);
            this.ClientNameLabel.Text = "이름:";
            // 
            // ClientNameTextBox
            // 
            this.ClientNameTextBox.Name = "ClientNameTextBox";
            this.ClientNameTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // AddButton
            // 
            this.AddButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.AddButton.Image = ((System.Drawing.Image)(resources.GetObject("AddButton.Image")));
            this.AddButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(99, 22);
            this.AddButton.Text = "클라이언트 추가";
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // RenameClientButton
            // 
            this.RenameClientButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.RenameClientButton.Name = "RenameClientButton";
            this.RenameClientButton.Size = new System.Drawing.Size(63, 22);
            this.RenameClientButton.Text = "이름 변경";
            this.RenameClientButton.Click += new System.EventHandler(this.RenameClientButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // WebsocketButton
            // 
            this.WebsocketButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.WebsocketButton.Image = ((System.Drawing.Image)(resources.GetObject("WebsocketButton.Image")));
            this.WebsocketButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.WebsocketButton.Name = "WebsocketButton";
            this.WebsocketButton.Size = new System.Drawing.Size(107, 22);
            this.WebsocketButton.Text = "웹소켓클라이언트";
            this.WebsocketButton.ToolTipText = "웹소켓클라이언트(VHR) ConstellationsOn";
            this.WebsocketButton.Click += new System.EventHandler(this.WebsocketButton_Click);
            // 
            // LogFolderButton
            // 
            this.LogFolderButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.LogFolderButton.Image = ((System.Drawing.Image)(resources.GetObject("LogFolderButton.Image")));
            this.LogFolderButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LogFolderButton.Name = "LogFolderButton";
            this.LogFolderButton.Size = new System.Drawing.Size(91, 22);
            this.LogFolderButton.Text = "로그 폴더 보기";
            this.LogFolderButton.Click += new System.EventHandler(this.LogFolderButton_Click);
            // 
            // DeleteEquipmentButton
            // 
            this.DeleteEquipmentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteEquipmentButton.Name = "DeleteEquipmentButton";
            this.DeleteEquipmentButton.Size = new System.Drawing.Size(127, 22);
            this.DeleteEquipmentButton.Text = "선택 클라이언트 삭제";
            this.DeleteEquipmentButton.ToolTipText = "선택 클라이언트 삭제";
            this.DeleteEquipmentButton.Click += new System.EventHandler(this.DeleteEquipmentButton_Click);
            // 
            // CopyEquipmentButton
            // 
            this.CopyEquipmentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.CopyEquipmentButton.Name = "CopyEquipmentButton";
            this.CopyEquipmentButton.Size = new System.Drawing.Size(99, 22);
            this.CopyEquipmentButton.Text = "클라이언트 복사";
            this.CopyEquipmentButton.Click += new System.EventHandler(this.CopyEquipmentButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // hexGroupLabel
            // 
            this.hexGroupLabel.Name = "hexGroupLabel";
            this.hexGroupLabel.Size = new System.Drawing.Size(55, 22);
            this.hexGroupLabel.Text = "Hex/line:";
            // 
            // hexBtn4
            // 
            this.hexBtn4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hexBtn4.Name = "hexBtn4";
            this.hexBtn4.Size = new System.Drawing.Size(23, 19);
            this.hexBtn4.Text = "4";
            this.hexBtn4.ToolTipText = "4 bytes per line";
            this.hexBtn4.Click += new System.EventHandler(this.hexBytesPerLine_Click);
            // 
            // hexBtn8
            // 
            this.hexBtn8.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hexBtn8.Name = "hexBtn8";
            this.hexBtn8.Size = new System.Drawing.Size(23, 19);
            this.hexBtn8.Text = "8";
            this.hexBtn8.ToolTipText = "8 bytes per line";
            this.hexBtn8.Click += new System.EventHandler(this.hexBytesPerLine_Click);
            // 
            // hexBtn16
            // 
            this.hexBtn16.Checked = true;
            this.hexBtn16.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hexBtn16.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hexBtn16.Name = "hexBtn16";
            this.hexBtn16.Size = new System.Drawing.Size(25, 19);
            this.hexBtn16.Text = "16";
            this.hexBtn16.ToolTipText = "16 bytes per line";
            this.hexBtn16.Click += new System.EventHandler(this.hexBytesPerLine_Click);
            // 
            // hexBtn32
            // 
            this.hexBtn32.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hexBtn32.Name = "hexBtn32";
            this.hexBtn32.Size = new System.Drawing.Size(25, 19);
            this.hexBtn32.Text = "32";
            this.hexBtn32.ToolTipText = "32 bytes per line";
            this.hexBtn32.Click += new System.EventHandler(this.hexBytesPerLine_Click);
            // 
            // hexBtn64
            // 
            this.hexBtn64.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.hexBtn64.Name = "hexBtn64";
            this.hexBtn64.Size = new System.Drawing.Size(25, 19);
            this.hexBtn64.Text = "64";
            this.hexBtn64.ToolTipText = "64 bytes per line";
            this.hexBtn64.Click += new System.EventHandler(this.hexBytesPerLine_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 383);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MainForm";
            this.Text = "EquipMonitor";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Maintab;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel ClientNameLabel;
        private System.Windows.Forms.ToolStripTextBox ClientNameTextBox;
        private System.Windows.Forms.ToolStripButton AddButton;
        private System.Windows.Forms.ToolStripButton RenameClientButton;
        private System.Windows.Forms.ToolStripButton WebsocketButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton LogFolderButton;
        private System.Windows.Forms.ToolStripButton DeleteEquipmentButton;
        private System.Windows.Forms.ToolStripButton CopyEquipmentButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel hexGroupLabel;
        private System.Windows.Forms.ToolStripButton hexBtn4;
        private System.Windows.Forms.ToolStripButton hexBtn8;
        private System.Windows.Forms.ToolStripButton hexBtn16;
        private System.Windows.Forms.ToolStripButton hexBtn32;
        private System.Windows.Forms.ToolStripButton hexBtn64;
    }
}
