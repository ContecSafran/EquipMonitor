using System;
using System.Linq;
using System.Windows.Forms;

namespace EquipMonitor
{
    public partial class CheckSumCalculator : UserControl
    {
        private TextBox _targetTextBox;

        public TextBox TargetTextBox
        {
            get { return _targetTextBox; }
            set
            {
                if (_targetTextBox != null)
                {
                    _targetTextBox.MouseUp -= TargetTextBox_SelectionChanged;
                    _targetTextBox.KeyUp -= TargetTextBox_SelectionChanged;
                }
                _targetTextBox = value;
                if (_targetTextBox != null)
                {
                    _targetTextBox.MouseUp += TargetTextBox_SelectionChanged;
                    _targetTextBox.KeyUp += TargetTextBox_SelectionChanged;
                }
            }
        }

        public CheckSumCalculator()
        {
            InitializeComponent();
            cmbChecksumMethod.SelectedIndex = 0; // Modular Sum
            cmbChecksumMethod.SelectedIndexChanged += CmbChecksumMethod_SelectedIndexChanged;
        }

        private void CmbChecksumMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateChecksum();
        }

        private void TargetTextBox_SelectionChanged(object sender, EventArgs e)
        {
            CalculateChecksum();
        }

        private void CalculateChecksum()
        {
            if (_targetTextBox == null) return;

            string selectedText = _targetTextBox.SelectedText;
            if (string.IsNullOrWhiteSpace(selectedText))
            {
                txtResult.Text = "";
                lblInfo.Text = "MessageTextBox에서 문자열을 블록 지정하세요.";
                return;
            }

            try
            {
                // 공백 등 제거하여 순수 헥사 문자열 추출
                string hexOnly = selectedText.Replace(" ", "").Replace("\r", "").Replace("\n", "");
                
                // 길이가 홀수면 마지막 한 글자는 제외
                if (hexOnly.Length % 2 != 0)
                {
                    hexOnly = hexOnly.Substring(0, hexOnly.Length - 1);
                }

                if (string.IsNullOrEmpty(hexOnly))
                {
                    txtResult.Text = "";
                    lblInfo.Text = "유효한 Hex 문자열이 아닙니다.";
                    return;
                }

                byte[] bytes = new byte[hexOnly.Length / 2];
                for (int i = 0; i < hexOnly.Length; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(hexOnly.Substring(i, 2), 16);
                }

                if (cmbChecksumMethod.Text.Contains("Modular Sum"))
                {
                    int sum = 0;
                    foreach (byte b in bytes)
                    {
                        sum += b;
                    }
                    byte checksum = (byte)(sum & 0xFF);
                    txtResult.Text = checksum.ToString("X2");
                }
                else if (cmbChecksumMethod.Text.Contains("XOR"))
                {
                    byte checksum = 0;
                    foreach (byte b in bytes)
                    {
                        checksum ^= b;
                    }
                    txtResult.Text = checksum.ToString("X2");
                }

                lblInfo.Text = $"{bytes.Length}바이트 계산 완료";
            }
            catch (Exception ex)
            {
                txtResult.Text = "Error";
                lblInfo.Text = "변환 오류: " + ex.Message;
            }
        }
    }
}
