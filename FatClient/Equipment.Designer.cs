
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MessageTextBox = new System.Windows.Forms.TextBox();
            this.ResponseTextBox = new System.Windows.Forms.TextBox();
            this.SendMessageButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.EquipmentTableLayout.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // EquipmentTableLayout
            // 
            this.EquipmentTableLayout.ColumnCount = 5;
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 269F));
            this.EquipmentTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.EquipmentTableLayout.Controls.Add(this.panel2, 2, 1);
            this.EquipmentTableLayout.Controls.Add(this.label3, 2, 0);
            this.EquipmentTableLayout.Controls.Add(this.ipText, 0, 1);
            this.EquipmentTableLayout.Controls.Add(this.portLabel, 1, 0);
            this.EquipmentTableLayout.Controls.Add(this.portTextBox, 0, 1);
            this.EquipmentTableLayout.Controls.Add(this.IPLabel, 0, 0);
            this.EquipmentTableLayout.Controls.Add(this.panel1, 3, 1);
            this.EquipmentTableLayout.Controls.Add(this.label2, 3, 0);
            this.EquipmentTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EquipmentTableLayout.Location = new System.Drawing.Point(3, 3);
            this.EquipmentTableLayout.Name = "EquipmentTableLayout";
            this.EquipmentTableLayout.RowCount = 2;
            this.EquipmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.EquipmentTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EquipmentTableLayout.Size = new System.Drawing.Size(824, 54);
            this.EquipmentTableLayout.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.udpRadio);
            this.panel2.Controls.Add(this.tcpRadio);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(403, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(152, 28);
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
            this.label3.Location = new System.Drawing.Point(403, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "통신 방식";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ipText
            // 
            this.ipText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ipText.Location = new System.Drawing.Point(3, 23);
            this.ipText.Name = "ipText";
            this.ipText.Size = new System.Drawing.Size(194, 21);
            this.ipText.TabIndex = 6;
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portLabel.Location = new System.Drawing.Point(203, 0);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(194, 20);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // portTextBox
            // 
            this.portTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.portTextBox.Location = new System.Drawing.Point(203, 23);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(194, 21);
            this.portTextBox.TabIndex = 0;
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IPLabel.Location = new System.Drawing.Point(3, 0);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(194, 20);
            this.IPLabel.TabIndex = 1;
            this.IPLabel.Text = "IP";
            this.IPLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.isAsciiMassage);
            this.panel1.Controls.Add(this.isHexMassage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(561, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(263, 28);
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
            this.label2.Location = new System.Drawing.Point(561, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(263, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "메시지 타입";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 830F));
            this.tableLayoutPanel1.Controls.Add(this.MessageTextBox, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.ResponseTextBox, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.SendMessageButton, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.EquipmentTableLayout, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(830, 429);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MessageTextBox.Location = new System.Drawing.Point(3, 93);
            this.MessageTextBox.Multiline = true;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.MessageTextBox.Size = new System.Drawing.Size(824, 133);
            this.MessageTextBox.TabIndex = 11;
            // 
            // ResponseTextBox
            // 
            this.ResponseTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResponseTextBox.Location = new System.Drawing.Point(3, 292);
            this.ResponseTextBox.Multiline = true;
            this.ResponseTextBox.Name = "ResponseTextBox";
            this.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResponseTextBox.Size = new System.Drawing.Size(824, 134);
            this.ResponseTextBox.TabIndex = 10;
            // 
            // SendMessageButton
            // 
            this.SendMessageButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendMessageButton.Location = new System.Drawing.Point(0, 229);
            this.SendMessageButton.Margin = new System.Windows.Forms.Padding(0);
            this.SendMessageButton.Name = "SendMessageButton";
            this.SendMessageButton.Size = new System.Drawing.Size(830, 60);
            this.SendMessageButton.TabIndex = 9;
            this.SendMessageButton.Text = "메시지 전송";
            this.SendMessageButton.UseVisualStyleBackColor = true;
            this.SendMessageButton.Click += new System.EventHandler(this.SendMessageButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(824, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Message";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(830, 429);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(830, 429);
            this.tableLayoutPanel3.TabIndex = 6;
            // 
            // Equipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "Equipment";
            this.Size = new System.Drawing.Size(830, 429);
            this.EquipmentTableLayout.ResumeLayout(false);
            this.EquipmentTableLayout.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button SendMessageButton;
        private System.Windows.Forms.TextBox MessageTextBox;
        private System.Windows.Forms.TextBox ResponseTextBox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton udpRadio;
        private System.Windows.Forms.RadioButton tcpRadio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton isAsciiMassage;
        private System.Windows.Forms.RadioButton isHexMassage;
        private System.Windows.Forms.Label label2;
    }
}
