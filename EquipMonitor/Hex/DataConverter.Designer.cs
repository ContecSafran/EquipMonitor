namespace EquipMonitor
{
    partial class DataConverter
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbEndian;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.TextBox txtValue;

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
            this.cmbEndian = new System.Windows.Forms.ComboBox();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cmbEndian);
            this.flowLayoutPanel1.Controls.Add(this.cmbDataType);
            this.flowLayoutPanel1.Controls.Add(this.txtValue);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(511, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // cmbEndian
            // 
            this.cmbEndian.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEndian.FormattingEnabled = true;
            this.cmbEndian.Items.AddRange(new object[] {
            "Big Endian",
            "Little Endian"});
            this.cmbEndian.Location = new System.Drawing.Point(3, 3);
            this.cmbEndian.Name = "cmbEndian";
            this.cmbEndian.Size = new System.Drawing.Size(100, 20);
            this.cmbEndian.TabIndex = 0;
            // 
            // cmbDataType
            // 
            this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Items.AddRange(new object[] {
            "2진수 (8비트)",
            "Int8",
            "UInt8",
            "Int16",
            "UInt16",
            "Int24",
            "UInt24",
            "Int32",
            "UInt32",
            "Int64",
            "UInt64",
            "Single (float32)",
            "Double (float64)"});
            this.cmbDataType.Location = new System.Drawing.Point(109, 3);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(120, 20);
            this.cmbDataType.TabIndex = 1;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(235, 3);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(260, 21);
            this.txtValue.TabIndex = 2;
            // 
            // DataConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DataConverter";
            this.Size = new System.Drawing.Size(511, 30);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
