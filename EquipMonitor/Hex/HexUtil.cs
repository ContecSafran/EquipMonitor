using System;
using System.Windows.Forms;

namespace EquipMonitor
{
    public partial class HexUtil : UserControl
    {
        private DataConverter dataConverter;
        private CheckSumCalculator checkSumCalculator;

        public TextBox TargetTextBox
        {
            get { return dataConverter?.TargetTextBox; }
            set 
            { 
                if (dataConverter != null) 
                    dataConverter.TargetTextBox = value; 
                if (checkSumCalculator != null)
                    checkSumCalculator.TargetTextBox = value;
            }
        }

        public HexUtil()
        {
            InitializeComponent();
            InitSubControls();
        }

        private void InitSubControls()
        {
            dataConverter = new DataConverter();
            checkSumCalculator = new CheckSumCalculator();

            dataConverter.Dock = DockStyle.Fill;
            checkSumCalculator.Dock = DockStyle.Fill;

            containerPanel.Controls.Add(dataConverter);
            containerPanel.Controls.Add(checkSumCalculator);

            subSelectorComboBox.Items.Clear();
            subSelectorComboBox.Items.Add("데이터 변환기");
            subSelectorComboBox.Items.Add("CheckSum 계산기");

            subSelectorComboBox.SelectedIndexChanged += SubSelectorComboBox_SelectedIndexChanged;

            if (subSelectorComboBox.Items.Count > 0)
            {
                subSelectorComboBox.SelectedIndex = 0;
            }
        }

        private void SubSelectorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selected = subSelectorComboBox.SelectedItem as string;
            if (selected == "데이터 변환기")
            {
                dataConverter.Visible = true;
                checkSumCalculator.Visible = false;
                dataConverter.BringToFront();
            }
            else if (selected == "CheckSum 계산기")
            {
                dataConverter.Visible = false;
                checkSumCalculator.Visible = true;
                checkSumCalculator.BringToFront();
            }
            else
            {
                dataConverter.Visible = false;
                checkSumCalculator.Visible = false;
            }
        }
    }
}
