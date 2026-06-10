namespace EquipMonitor
{
    partial class HexUtil
    {
        private System.ComponentModel.IContainer components = null;

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
            this.subSelectorComboBox = new System.Windows.Forms.ComboBox();
            this.containerPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // subSelectorComboBox
            // 
            this.subSelectorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.subSelectorComboBox.FormattingEnabled = true;
            this.subSelectorComboBox.Location = new System.Drawing.Point(5, 5);
            this.subSelectorComboBox.Name = "subSelectorComboBox";
            this.subSelectorComboBox.Size = new System.Drawing.Size(120, 20);
            this.subSelectorComboBox.TabIndex = 0;
            // 
            // containerPanel
            // 
            this.containerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.containerPanel.Location = new System.Drawing.Point(130, 0);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(511, 30);
            this.containerPanel.TabIndex = 1;
            // 
            // HexUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.containerPanel);
            this.Controls.Add(this.subSelectorComboBox);
            this.Name = "HexUtil";
            this.Size = new System.Drawing.Size(641, 30);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox subSelectorComboBox;
        private System.Windows.Forms.Panel containerPanel;
    }
}
