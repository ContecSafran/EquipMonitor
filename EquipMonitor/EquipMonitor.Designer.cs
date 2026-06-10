
namespace EquipMonitor
{
    partial class EquipMonitor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EquipMonitor));
            this.Maintab = new System.Windows.Forms.TabControl();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.AddClientNameTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.AddButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.WebsocketButton = new System.Windows.Forms.ToolStripButton();
            this.LogFolderButton = new System.Windows.Forms.ToolStripButton();
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
            this.Maintab.Size = new System.Drawing.Size(822, 358);
            this.Maintab.TabIndex = 5;
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.Maintab);
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
            this.AddClientNameTextBox,
            this.AddButton,
            this.toolStripSeparator1,
            this.WebsocketButton,
            this.LogFolderButton});
            this.toolStrip1.Location = new System.Drawing.Point(3, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(417, 25);
            this.toolStrip1.TabIndex = 0;
            // 
            // AddClientNameTextBox
            // 
            this.AddClientNameTextBox.Font = new System.Drawing.Font("맑은 고딕", 9F);
            this.AddClientNameTextBox.Name = "AddClientNameTextBox";
            this.AddClientNameTextBox.Size = new System.Drawing.Size(100, 25);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 383);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "Form1";
            this.Text = "EquipMonitor";
            this.Load += new System.EventHandler(this.Form1_Load);
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
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton AddButton;
        private System.Windows.Forms.ToolStripButton WebsocketButton;
        private System.Windows.Forms.ToolStripTextBox AddClientNameTextBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton LogFolderButton;
    }
}

