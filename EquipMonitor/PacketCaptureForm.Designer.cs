namespace EquipMonitor
{
    partial class PacketCaptureForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.nicLabel = new System.Windows.Forms.ToolStripLabel();
            this.nicComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.startStopButton = new System.Windows.Forms.ToolStripButton();
            this.clearButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.filterLabel = new System.Windows.Forms.ToolStripLabel();
            this.filterTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.autoScrollButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.packetListView = new System.Windows.Forms.ListView();
            this.colNo = new System.Windows.Forms.ColumnHeader();
            this.colTime = new System.Windows.Forms.ColumnHeader();
            this.colProtocol = new System.Windows.Forms.ColumnHeader();
            this.colSource = new System.Windows.Forms.ColumnHeader();
            this.colDest = new System.Windows.Forms.ColumnHeader();
            this.colInfo = new System.Windows.Forms.ColumnHeader();
            this.detailTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();

            // toolStrip1
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.nicLabel,
                this.nicComboBox,
                this.startStopButton,
                this.clearButton,
                this.toolStripSep1,
                this.filterLabel,
                this.filterTextBox,
                this.autoScrollButton
            });
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.TabIndex = 0;

            // nicLabel
            this.nicLabel.Name = "nicLabel";
            this.nicLabel.Text = "NIC:";

            // nicComboBox
            this.nicComboBox.Name = "nicComboBox";
            this.nicComboBox.Size = new System.Drawing.Size(220, 25);
            this.nicComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // startStopButton
            this.startStopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Text = "▶ 시작";
            this.startStopButton.Click += new System.EventHandler(this.startStopButton_Click);

            // clearButton
            this.clearButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.clearButton.Name = "clearButton";
            this.clearButton.Text = "초기화";
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);

            // toolStripSep1
            this.toolStripSep1.Name = "toolStripSep1";

            // filterLabel
            this.filterLabel.Name = "filterLabel";
            this.filterLabel.Text = "필터:";

            // filterTextBox
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(160, 25);
            this.filterTextBox.ToolTipText = "IP, 포트, 프로토콜, 내용으로 필터링";

            // autoScrollButton
            this.autoScrollButton.CheckOnClick = true;
            this.autoScrollButton.Checked = true;
            this.autoScrollButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.autoScrollButton.Name = "autoScrollButton";
            this.autoScrollButton.Text = "자동 스크롤";

            // splitContainer1
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.packetListView);
            this.splitContainer1.Panel2.Controls.Add(this.detailTextBox);
            this.splitContainer1.SplitterDistance = 420;
            this.splitContainer1.TabIndex = 1;

            // packetListView
            this.packetListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                this.colNo, this.colTime, this.colProtocol, this.colSource, this.colDest, this.colInfo
            });
            this.packetListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.packetListView.FullRowSelect = true;
            this.packetListView.GridLines = true;
            this.packetListView.HideSelection = false;
            this.packetListView.Name = "packetListView";
            this.packetListView.View = System.Windows.Forms.View.Details;
            this.packetListView.SelectedIndexChanged += new System.EventHandler(this.packetListView_SelectedIndexChanged);

            this.colNo.Text = "No.";
            this.colNo.Width = 55;
            this.colTime.Text = "시간";
            this.colTime.Width = 105;
            this.colProtocol.Text = "프로토콜";
            this.colProtocol.Width = 80;
            this.colSource.Text = "출발지";
            this.colSource.Width = 175;
            this.colDest.Text = "목적지";
            this.colDest.Width = 175;
            this.colInfo.Text = "정보";
            this.colInfo.Width = 420;

            // detailTextBox
            this.detailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.detailTextBox.Font = new System.Drawing.Font("Consolas", 9F);
            this.detailTextBox.Multiline = true;
            this.detailTextBox.Name = "detailTextBox";
            this.detailTextBox.ReadOnly = true;
            this.detailTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.detailTextBox.WordWrap = false;
            this.detailTextBox.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.detailTextBox.ForeColor = System.Drawing.Color.LightGreen;

            // statusStrip1
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabel });
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.TabIndex = 2;

            // statusLabel
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Text = "준비 (관리자 권한 필요)";
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // PacketCaptureForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 680);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "PacketCaptureForm";
            this.Text = "패킷 캡처 (Raw Socket) — 관리자 권한 필요";

            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel nicLabel;
        private System.Windows.Forms.ToolStripComboBox nicComboBox;
        private System.Windows.Forms.ToolStripButton startStopButton;
        private System.Windows.Forms.ToolStripButton clearButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSep1;
        private System.Windows.Forms.ToolStripLabel filterLabel;
        private System.Windows.Forms.ToolStripTextBox filterTextBox;
        private System.Windows.Forms.ToolStripButton autoScrollButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView packetListView;
        private System.Windows.Forms.ColumnHeader colNo;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colProtocol;
        private System.Windows.Forms.ColumnHeader colSource;
        private System.Windows.Forms.ColumnHeader colDest;
        private System.Windows.Forms.ColumnHeader colInfo;
        private System.Windows.Forms.TextBox detailTextBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    }
}
