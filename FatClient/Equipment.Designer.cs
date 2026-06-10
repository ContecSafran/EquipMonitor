
namespace FatClient
{
    partial class Equipment
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.EquipmentTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.sendBinaryFileButton = new System.Windows.Forms.Button();
            this.tailTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.udpRadio = new System.Windows.Forms.RadioButton();
            this.tcpRadio = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ipText = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.IPLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.isAsciiMassage = new System.Windows.Forms.RadioButton();
            this.isHexMassage = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.selectBinaryFileButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.commandLayout = new System.Windows.Forms.TableLayoutPanel();
            this.commandListManageLayout = new System.Windows.Forms.TableLayoutPanel();
            this.commandListBox = new System.Windows.Forms.ListBox();
            this.commandButtonsLayout = new System.Windows.Forms.TableLayoutPanel();
            this.addCommandButton = new System.Windows.Forms.Button();
            this.deleteCommandButton = new System.Windows.Forms.Button();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.logTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.ResponseAsciiTextBox = new System.Windows.Forms.TextBox();
            this.ResponseHexTextBox = new System.Windows.Forms.TextBox();
            this.EquipmentTableLayout.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.commandLayout.SuspendLayout();
            this.commandListManageLayout.SuspendLayout();
            this.commandButtonsLayout.SuspendLayout();
            this.logTableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // EquipmentTableLayout
            // 
            this.EquipmentTableLayout.ColumnCount = 7;
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.EquipmentTableLayout.Controls.Add(this.disconnectButton, 6, 1);
            this.EquipmentTableLayout.Controls.Add(this.connectButton, 6, 0);
            this.EquipmentTableLayout.Controls.Add(this.sendBinaryFileButton, 5, 1);
            this.EquipmentTableLayout.Controls.Add(this.tailTextBox, 4, 1);
            this.EquipmentTableLayout.Controls.Add(this.label4, 4, 0);
            this.EquipmentTableLayout.Controls.Add(this.panel2, 2, 1);
            this.EquipmentTableLayout.Controls.Add(this.label3, 2, 0);
            this.EquipmentTableLayout.Controls.Add(this.ipText, 0, 1);
            this.EquipmentTableLayout.Controls.Add(this.portLabel, 1, 0);
            this.EquipmentTableLayout.Controls.Add(this.portTextBox, 1, 1);
            this.EquipmentTableLayout.Controls.Add(this.IPLabel, 0, 0);
            this.EquipmentTableLayout.Controls.Add(this.panel1, 3, 1);
            this.EquipmentTableLayout.Controls.Add(this.label2, 3, 0);
            this.EquipmentTableLayout.Controls.Add(this.selectBinaryFileButton, 5, 0);
            this.EquipmentTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EquipmentTableLayout.Location = new System.Drawing.Point(3, 3);
            this.EquipmentTableLayout.Name = "EquipmentTableLayout";
            this.EquipmentTableLayout.RowCount = 2;
            this.EquipmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.EquipmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EquipmentTableLayout.Size = new System.Drawing.Size(924, 64);
            this.EquipmentTableLayout.TabIndex = 0;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(841, 32);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(98, 23);
            this.disconnectButton.TabIndex = 21;
            this.disconnectButton.Text = "DisConnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(841, 3);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(98, 23);
            this.connectButton.TabIndex = 20;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // sendBinaryFileButton
            // 
            this.sendBinaryFileButton.Location = new System.Drawing.Point(713, 32);
            this.sendBinaryFileButton.Name = "sendBinaryFileButton";
            this.sendBinaryFileButton.Size = new System.Drawing.Size(122, 23);
            this.sendBinaryFileButton.TabIndex = 19;
            this.sendBinaryFileButton.Text = "선택된 파일 전송";
            this.sendBinaryFileButton.UseVisualStyleBackColor = true;
            this.sendBinaryFileButton.Click += new System.EventHandler(this.button2_ClickAsync);
            // 
            // tailTextBox
            // 
            this.tailTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tailTextBox.Location = new System.Drawing.Point(563, 32);
            this.tailTextBox.Name = "tailTextBox";
            this.tailTextBox.Size = new System.Drawing.Size(144, 21);
            this.tailTextBox.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(563, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 29);
            this.label4.TabIndex = 14;
            this.label4.Text = "Tail 문자열(Ascii만 해당)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.udpRadio);
            this.panel2.Controls.Add(this.tcpRadio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(303, 32);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(124, 29);
            this.panel2.TabIndex = 13;
            // 
            // udpRadio
            // 
            this.udpRadio.AutoSize = true;
            this.udpRadio.Location = new System.Drawing.Point(54, 6);
            this.udpRadio.Name = "udpRadio";
            this.udpRadio.Size = new System.Drawing.Size(47, 16);
            this.udpRadio.TabIndex = 9;
            this.udpRadio.TabStop = true;
            this.udpRadio.Text = "UDP";
            this.udpRadio.UseVisualStyleBackColor = true;
            // 
            // tcpRadio
            // 
            this.tcpRadio.AutoSize = true;
            this.tcpRadio.Location = new System.Drawing.Point(3, 7);
            this.tcpRadio.Name = "tcpRadio";
            this.tcpRadio.Size = new System.Drawing.Size(48, 16);
            this.tcpRadio.TabIndex = 8;
            this.tcpRadio.TabStop = true;
            this.tcpRadio.Text = "TCP";
            this.tcpRadio.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(303, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 29);
            this.label3.TabIndex = 12;
            this.label3.Text = "통신 방식";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ipText
            // 
            this.ipText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipText.Location = new System.Drawing.Point(3, 32);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(144, 21);
            this.ipText.TabIndex = 6;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portLabel.Location = new System.Drawing.Point(153, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(144, 29);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portTextBox
            // 
            this.portTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portTextBox.Location = new System.Drawing.Point(153, 32);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(144, 21);
            this.portTextBox.TabIndex = 0;
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IPLabel.Location = new System.Drawing.Point(3, 0);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(144, 29);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "IP";
            this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.isAsciiMassage);
            this.panel1.Controls.Add(this.isHexMassage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(433, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(124, 29);
            this.panel1.TabIndex = 10;
            // 
            // isAsciiMassage
            // 
            this.isAsciiMassage.AutoSize = true;
            this.isAsciiMassage.Location = new System.Drawing.Point(54, 8);
            this.isAsciiMassage.Name = "isAsciiMassage";
            this.isAsciiMassage.Size = new System.Drawing.Size(51, 16);
            this.isAsciiMassage.TabIndex = 9;
            this.isAsciiMassage.TabStop = true;
            this.isAsciiMassage.Text = "Ascii";
            this.isAsciiMassage.UseVisualStyleBackColor = true;
            // 
            // isHexMassage
            // 
            this.isHexMassage.AutoSize = true;
            this.isHexMassage.Location = new System.Drawing.Point(3, 7);
            this.isHexMassage.Name = "isHexMassage";
            this.isHexMassage.Size = new System.Drawing.Size(45, 16);
            this.isHexMassage.TabIndex = 8;
            this.isHexMassage.TabStop = true;
            this.isHexMassage.Text = "Hex";
            this.isHexMassage.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(433, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "메시지 타입";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectBinaryFileButton
            // 
            this.selectBinaryFileButton.AutoSize = true;
            this.selectBinaryFileButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectBinaryFileButton.Location = new System.Drawing.Point(713, 3);
            this.selectBinaryFileButton.Name = "selectBinaryFileButton";
            this.selectBinaryFileButton.Size = new System.Drawing.Size(122, 23);
            this.selectBinaryFileButton.TabIndex = 18;
            this.selectBinaryFileButton.Text = "바이너리 파일 선택";
            this.selectBinaryFileButton.UseVisualStyleBackColor = true;
            this.selectBinaryFileButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.commandLayout, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.EquipmentTableLayout, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SendMessageButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.logTableLayoutPanel, 0, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(930, 452);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // commandLayout
            // 
            this.commandLayout.ColumnCount = 2;
            this.commandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.commandLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.commandLayout.Controls.Add(this.commandListManageLayout, 0, 0);
            this.commandLayout.Controls.Add(this.MessageTextBox, 1, 0);
            this.commandLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandLayout.Location = new System.Drawing.Point(3, 93);
            this.commandLayout.Name = "commandLayout";
            this.commandLayout.RowCount = 1;
            this.commandLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandLayout.Size = new System.Drawing.Size(924, 160);
            this.commandLayout.TabIndex = 12;
            // 
            // commandListManageLayout
            // 
            this.commandListManageLayout.ColumnCount = 1;
            this.commandListManageLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandListManageLayout.Controls.Add(this.commandListBox, 0, 0);
            this.commandListManageLayout.Controls.Add(this.commandButtonsLayout, 0, 1);
            this.commandListManageLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandListManageLayout.Location = new System.Drawing.Point(0, 0);
            this.commandListManageLayout.Margin = new System.Windows.Forms.Padding(0);
            this.commandListManageLayout.Name = "commandListManageLayout";
            this.commandListManageLayout.RowCount = 2;
            this.commandListManageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandListManageLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.commandListManageLayout.Size = new System.Drawing.Size(277, 160);
            this.commandListManageLayout.TabIndex = 0;
            // 
            // commandListBox
            // 
            this.commandListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandListBox.FormattingEnabled = true;
            this.commandListBox.ItemHeight = 12;
            this.commandListBox.Location = new System.Drawing.Point(3, 3);
            this.commandListBox.Name = "commandListBox";
            this.commandListBox.Size = new System.Drawing.Size(271, 124);
            this.commandListBox.TabIndex = 0;
            this.commandListBox.SelectedIndexChanged += new System.EventHandler(this.commandListBox_SelectedIndexChanged);
            // 
            // commandButtonsLayout
            // 
            this.commandButtonsLayout.ColumnCount = 2;
            this.commandButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandButtonsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.commandButtonsLayout.Controls.Add(this.addCommandButton, 0, 0);
            this.commandButtonsLayout.Controls.Add(this.deleteCommandButton, 1, 0);
            this.commandButtonsLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.commandButtonsLayout.Location = new System.Drawing.Point(0, 130);
            this.commandButtonsLayout.Margin = new System.Windows.Forms.Padding(0);
            this.commandButtonsLayout.Name = "commandButtonsLayout";
            this.commandButtonsLayout.RowCount = 1;
            this.commandButtonsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.commandButtonsLayout.Size = new System.Drawing.Size(277, 30);
            this.commandButtonsLayout.TabIndex = 1;
            // 
            // addCommandButton
            // 
            this.addCommandButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.addCommandButton.Location = new System.Drawing.Point(3, 3);
            this.addCommandButton.Name = "addCommandButton";
            this.addCommandButton.Size = new System.Drawing.Size(132, 24);
            this.addCommandButton.TabIndex = 0;
            this.addCommandButton.Text = "추가";
            this.addCommandButton.UseVisualStyleBackColor = true;
            this.addCommandButton.Click += new System.EventHandler(this.addCommandButton_Click);
            // 
            // deleteCommandButton
            // 
            this.deleteCommandButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.deleteCommandButton.Location = new System.Drawing.Point(141, 3);
            this.deleteCommandButton.Name = "deleteCommandButton";
            this.deleteCommandButton.Size = new System.Drawing.Size(133, 24);
            this.deleteCommandButton.TabIndex = 1;
            this.deleteCommandButton.Text = "삭제";
            this.deleteCommandButton.UseVisualStyleBackColor = true;
            this.deleteCommandButton.Click += new System.EventHandler(this.deleteCommandButton_Click);
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Location = new System.Drawing.Point(280, 3);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MessageTextBox.Size = new System.Drawing.Size(641, 154);
            this.MessageTextBox.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(924, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendMessageButton.Location = new System.Drawing.Point(0, 256);
            this.SendMessageButton.Margin = new System.Windows.Forms.Padding(0);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(930, 30);
            this.SendMessageButton.TabIndex = 9;
            this.SendMessageButton.Text = "메시지 전송";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // logTableLayoutPanel
            // 
            this.logTableLayoutPanel.ColumnCount = 2;
            this.logTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.logTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.logTableLayoutPanel.Controls.Add(this.ResponseAsciiTextBox, 1, 0);
            this.logTableLayoutPanel.Controls.Add(this.ResponseHexTextBox, 0, 0);
            this.logTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTableLayoutPanel.Location = new System.Drawing.Point(3, 289);
            this.logTableLayoutPanel.Name = "logTableLayoutPanel";
            this.logTableLayoutPanel.RowCount = 1;
            this.logTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.logTableLayoutPanel.Size = new System.Drawing.Size(924, 160);
            this.logTableLayoutPanel.TabIndex = 12;
            // 
            // ResponseAsciiTextBox
            // 
            this.ResponseAsciiTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseAsciiTextBox.Location = new System.Drawing.Point(465, 3);
            this.ResponseAsciiTextBox.Multiline = true;
            this.ResponseAsciiTextBox.Name = "ResponseAsciiTextBox";
            this.ResponseAsciiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResponseAsciiTextBox.Size = new System.Drawing.Size(456, 154);
            this.ResponseAsciiTextBox.TabIndex = 11;
            // 
            // ResponseHexTextBox
            // 
            this.ResponseHexTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseHexTextBox.Location = new System.Drawing.Point(3, 3);
            this.ResponseHexTextBox.Multiline = true;
            this.ResponseHexTextBox.Name = "ResponseHexTextBox";
            this.ResponseHexTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResponseHexTextBox.Size = new System.Drawing.Size(456, 154);
            this.ResponseHexTextBox.TabIndex = 10;
            // 
            // Equipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Equipment";
            this.Size = new System.Drawing.Size(930, 452);
            this.EquipmentTableLayout.ResumeLayout(false);
            this.EquipmentTableLayout.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.commandLayout.ResumeLayout(false);
            this.commandLayout.PerformLayout();
            this.commandListManageLayout.ResumeLayout(false);
            this.commandButtonsLayout.ResumeLayout(false);
            this.logTableLayoutPanel.ResumeLayout(false);
            this.logTableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel EquipmentTableLayout;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.TextBox ResponseHexTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton udpRadio;
        private System.Windows.Forms.RadioButton tcpRadio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton isAsciiMassage;
        private System.Windows.Forms.RadioButton isHexMassage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tailTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button selectBinaryFileButton;
        private System.Windows.Forms.Button sendBinaryFileButton;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TableLayoutPanel logTableLayoutPanel;
        private System.Windows.Forms.TextBox ResponseAsciiTextBox;
        private System.Windows.Forms.TableLayoutPanel commandLayout;
        private System.Windows.Forms.TableLayoutPanel commandListManageLayout;
        private System.Windows.Forms.ListBox commandListBox;
        private System.Windows.Forms.TableLayoutPanel commandButtonsLayout;
        private System.Windows.Forms.Button addCommandButton;
        private System.Windows.Forms.Button deleteCommandButton;
    }
}
