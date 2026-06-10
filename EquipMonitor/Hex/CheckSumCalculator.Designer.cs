namespace EquipMonitor
{
    partial class CheckSumCalculator
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbChecksumMethod;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.Label lblInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cmbChecksumMethod = new System.Windows.Forms.ComboBox();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbChecksumMethod);
            this.flowLayoutPanel1.Controls.Add(this.txtResult);
            this.flowLayoutPanel1.Controls.Add(this.lblInfo);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(511, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cmbChecksumMethod
            // 
            this.cmbChecksumMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbChecksumMethod.FormattingEnabled = true;
            this.cmbChecksumMethod.Items.AddRange(new object[] {
            "Modular Sum (8-bit)",
            "XOR (LRC)"});
            this.cmbChecksumMethod.Location = new System.Drawing.Point(3, 3);
            this.cmbChecksumMethod.Name = "cmbChecksumMethod";
            this.cmbChecksumMethod.Size = new System.Drawing.Size(150, 20);
            this.cmbChecksumMethod.TabIndex = 0;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(159, 3);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(100, 21);
            this.txtResult.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(265, 6);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(200, 12);
            this.lblInfo.TabIndex = 2;
            this.lblInfo.Text = "MessageTextBox에서 문자열을 블록 지정하세요.";
            // 
            // CheckSumCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CheckSumCalculator";
            this.Size = new System.Drawing.Size(511, 30);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
