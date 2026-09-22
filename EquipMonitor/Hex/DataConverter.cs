using System;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Text;

namespace EquipMonitor
{
    public partial class DataConverter : UserControl
    {
        private TextBoxBase _targetTextBox;
        private bool _isUpdatingText = false;

        public TextBoxBase TargetTextBox
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

        public DataConverter()
        {
            InitializeComponent();
            cmbEndian.SelectedIndex = 0; // Big Endian
            cmbDataType.SelectedIndex = 7; // Int32
            
            cmbEndian.SelectedIndexChanged += (s, e) => UpdateValueFromHex();
            cmbDataType.SelectedIndexChanged += (s, e) => UpdateValueFromHex();
            txtValue.TextChanged += TxtValue_TextChanged;
        }

        private void TargetTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (_isUpdatingText) return;
            UpdateValueFromHex();
        }

        private void UpdateValueFromHex()
        {
            if (_targetTextBox == null || string.IsNullOrEmpty(_targetTextBox.Text)) return;
            if (_isUpdatingText) return;

            try
            {
                _isUpdatingText = true;

                int byteIndex;
                string hexOnly;

                if (_targetTextBox is HexRichTextBox hexBox)
                {
                    byteIndex = hexBox.GetByteIndexFromPosition(_targetTextBox.SelectionStart);
                    hexOnly = hexBox.StripOffsets(_targetTextBox.Text)
                                    .Replace(" ", "").Replace("\r", "").Replace("\n", "");
                }
                else
                {
                    byteIndex = _targetTextBox.SelectionStart / 3;
                    hexOnly = _targetTextBox.Text.Replace(" ", "").Replace("\r", "").Replace("\n", "");
                }

                if (byteIndex * 2 >= hexOnly.Length) { txtValue.Text = ""; return; }

                int neededBytes = GetNeededBytes(cmbDataType.Text);
                if (neededBytes <= 0 || byteIndex * 2 + neededBytes * 2 > hexOnly.Length)
                {
                    txtValue.Text = "";
                    return;
                }

                string hexChunk = hexOnly.Substring(byteIndex * 2, neededBytes * 2);
                byte[] bytes = StringToByteArray(hexChunk);

                bool isLittleEndian = cmbEndian.Text == "Little Endian";
                if (BitConverter.IsLittleEndian != isLittleEndian)
                    Array.Reverse(bytes);

                txtValue.Text = BytesToValueString(bytes, cmbDataType.Text);
            }
            catch (Exception)
            {
                txtValue.Text = "Error";
            }
            finally
            {
                _isUpdatingText = false;
            }
        }

        private void TxtValue_TextChanged(object sender, EventArgs e)
        {
            if (_targetTextBox == null || _isUpdatingText || _targetTextBox.ReadOnly) return;

            try
            {
                _isUpdatingText = true;
                string valStr = txtValue.Text;
                if (string.IsNullOrWhiteSpace(valStr)) return;

                int neededBytes = GetNeededBytes(cmbDataType.Text);
                if (neededBytes <= 0) return;

                byte[] newBytes = ValueStringToBytes(valStr, cmbDataType.Text, neededBytes);
                if (newBytes == null) return;

                bool isLittleEndian = cmbEndian.Text == "Little Endian";
                if (BitConverter.IsLittleEndian != isLittleEndian)
                {
                    Array.Reverse(newBytes);
                }

                string hexChunk = BitConverter.ToString(newBytes).Replace("-", " ");

                int selectionStart = _targetTextBox.SelectionStart;
                string text = _targetTextBox.Text;
                int byteIndex = selectionStart / 3;

                string[] bytesArray = text.Split(new[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (byteIndex >= bytesArray.Length || byteIndex + neededBytes > bytesArray.Length) return;

                string[] newHexTokens = hexChunk.Split(' ');
                for (int i = 0; i < neededBytes; i++)
                {
                    bytesArray[byteIndex + i] = newHexTokens[i];
                }

                _targetTextBox.Text = string.Join(" ", bytesArray);
                _targetTextBox.SelectionStart = selectionStart;
            }
            catch
            {
            }
            finally
            {
                _isUpdatingText = false;
            }
        }

        private int GetNeededBytes(string dataType)
        {
            switch (dataType)
            {
                case "2진수 (8비트)":
                case "Int8":
                case "UInt8": return 1;
                case "Int16":
                case "UInt16": return 2;
                case "Int24":
                case "UInt24": return 3;
                case "Int32":
                case "UInt32":
                case "Single (float32)": return 4;
                case "Int64":
                case "UInt64":
                case "Double (float64)": return 8;
                default: return 0;
            }
        }

        private string BytesToValueString(byte[] bytes, string dataType)
        {
            switch (dataType)
            {
                case "2진수 (8비트)": return Convert.ToString(bytes[0], 2).PadLeft(8, '0');
                case "Int8": return ((sbyte)bytes[0]).ToString();
                case "UInt8": return bytes[0].ToString();
                case "Int16": return BitConverter.ToInt16(bytes, 0).ToString();
                case "UInt16": return BitConverter.ToUInt16(bytes, 0).ToString();
                case "Int24":
                    byte[] b24i = new byte[4];
                    if (BitConverter.IsLittleEndian)
                    {
                        b24i[0] = bytes[0]; b24i[1] = bytes[1]; b24i[2] = bytes[2];
                        if ((bytes[2] & 0x80) != 0) b24i[3] = 0xFF;
                    }
                    else
                    {
                        b24i[1] = bytes[0]; b24i[2] = bytes[1]; b24i[3] = bytes[2];
                        if ((bytes[0] & 0x80) != 0) b24i[0] = 0xFF;
                    }
                    return BitConverter.ToInt32(b24i, 0).ToString();
                case "UInt24":
                    byte[] b24u = new byte[4];
                    if (BitConverter.IsLittleEndian)
                    {
                        b24u[0] = bytes[0]; b24u[1] = bytes[1]; b24u[2] = bytes[2];
                    }
                    else
                    {
                        b24u[1] = bytes[0]; b24u[2] = bytes[1]; b24u[3] = bytes[2];
                    }
                    return BitConverter.ToUInt32(b24u, 0).ToString();
                case "Int32": return BitConverter.ToInt32(bytes, 0).ToString();
                case "UInt32": return BitConverter.ToUInt32(bytes, 0).ToString();
                case "Int64": return BitConverter.ToInt64(bytes, 0).ToString();
                case "UInt64": return BitConverter.ToUInt64(bytes, 0).ToString();
                case "Single (float32)": return BitConverter.ToSingle(bytes, 0).ToString("G");
                case "Double (float64)": return BitConverter.ToDouble(bytes, 0).ToString("G");
                default: return "";
            }
        }

        private byte[] ValueStringToBytes(string val, string dataType, int neededBytes)
        {
            byte[] bytes = new byte[neededBytes];
            byte[] temp;
            switch (dataType)
            {
                case "2진수 (8비트)":
                    bytes[0] = Convert.ToByte(val, 2);
                    break;
                case "Int8":
                    bytes[0] = (byte)sbyte.Parse(val);
                    break;
                case "UInt8":
                    bytes[0] = byte.Parse(val);
                    break;
                case "Int16":
                    temp = BitConverter.GetBytes(short.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "UInt16":
                    temp = BitConverter.GetBytes(ushort.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "Int24":
                    int i24 = int.Parse(val);
                    temp = BitConverter.GetBytes(i24);
                    if (BitConverter.IsLittleEndian)
                    {
                        bytes[0] = temp[0]; bytes[1] = temp[1]; bytes[2] = temp[2];
                    }
                    else
                    {
                        bytes[0] = temp[1]; bytes[1] = temp[2]; bytes[2] = temp[3];
                    }
                    break;
                case "UInt24":
                    uint u24 = uint.Parse(val);
                    temp = BitConverter.GetBytes(u24);
                    if (BitConverter.IsLittleEndian)
                    {
                        bytes[0] = temp[0]; bytes[1] = temp[1]; bytes[2] = temp[2];
                    }
                    else
                    {
                        bytes[0] = temp[1]; bytes[1] = temp[2]; bytes[2] = temp[3];
                    }
                    break;
                case "Int32":
                    temp = BitConverter.GetBytes(int.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "UInt32":
                    temp = BitConverter.GetBytes(uint.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "Int64":
                    temp = BitConverter.GetBytes(long.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "UInt64":
                    temp = BitConverter.GetBytes(ulong.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "Single (float32)":
                    temp = BitConverter.GetBytes(float.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                case "Double (float64)":
                    temp = BitConverter.GetBytes(double.Parse(val));
                    Array.Copy(temp, bytes, neededBytes);
                    break;
                default:
                    return null;
            }
            return bytes;
        }

        private byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0) return new byte[0];
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
