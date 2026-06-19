using System;
using System.Text;
using System.Windows.Forms;

namespace EquipMonitor
{
    public class HexRichTextBox : RichTextBox
    {
        // FormatHexView 의 오프셋 길이: "XXXXXXXX  " = 8 hex + 2 space
        public int OffsetLength { get; set; } = 10;
        public int BytesPerLine { get; set; } = 16;

        private bool _adjusting;

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            if (_adjusting || SelectionLength == 0) return;

            _adjusting = true;
            try { ClampSelection(); }
            finally { _adjusting = false; }
        }

        // 처음 OffsetLength자가 전부 공백이면 헤더 줄
        private bool IsHeaderLine(string line)
        {
            if (line.Length < OffsetLength) return false;
            for (int i = 0; i < OffsetLength; i++)
                if (line[i] != ' ') return false;
            return true;
        }

        // 첫 번째 데이터 줄의 시작 문자 인덱스 (헤더가 없으면 0)
        private int FirstDataCharIndex()
        {
            if (Lines.Length < 2) return 0;
            int idx = GetFirstCharIndexFromLine(1);
            return idx < 0 ? 0 : idx;
        }

        private void ClampSelection()
        {
            int selStart = SelectionStart;
            int selEnd   = selStart + SelectionLength;

            // 헤더 줄 선택 방지: 첫 번째 데이터 줄 이후로 고정
            int firstData = FirstDataCharIndex();
            int newStart  = Math.Max(selStart, firstData);

            // 선택 시작이 오프셋 영역이면 HEX 영역 시작으로 이동
            int startLine      = GetLineFromCharIndex(newStart);
            int startLineBegin = GetFirstCharIndexFromLine(startLine);
            newStart = Math.Max(newStart, startLineBegin + OffsetLength);

            // 선택 종료(exclusive)의 직전 문자가 오프셋 영역이면 그 줄의 HEX 영역 시작으로 이동
            int newEnd = selEnd;
            if (newEnd > newStart)
            {
                int endLine      = GetLineFromCharIndex(newEnd - 1);
                int endLineBegin = GetFirstCharIndexFromLine(endLine);
                int endHexStart  = endLineBegin + OffsetLength;

                if (newEnd - 1 < endHexStart)
                    newEnd = endHexStart;
            }

            if (newStart == selStart && newEnd == selEnd) return;
            if (newStart >= newEnd) { SelectionLength = 0; return; }

            Select(newStart, newEnd - newStart);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.C) ||
                keyData == (Keys.Control | Keys.Insert))
            {
                CopyHexOnly();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CopyHexOnly()
        {
            string selected = SelectedText;
            if (string.IsNullOrEmpty(selected)) return;

            string[] lines = selected.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var sb = new StringBuilder();
            foreach (string line in lines)
            {
                if (IsHeaderLine(line)) continue;           // 헤더 줄 제외
                if (sb.Length > 0) sb.AppendLine();
                sb.Append(HasOffsetPrefix(line) ? line.Substring(OffsetLength) : line);
            }

            string result = sb.ToString().TrimEnd();
            if (!string.IsNullOrEmpty(result))
                Clipboard.SetText(result);
        }

        public bool HasOffsetPrefix(string line)
        {
            if (line.Length < OffsetLength) return false;
            for (int i = 0; i < 8; i++)
                if (!Uri.IsHexDigit(line[i])) return false;
            return line[8] == ' ' && line[9] == ' ';
        }

        // 선택 텍스트 또는 전체 텍스트에서 헤더/오프셋을 제거하고 순수 HEX만 반환
        public string StripOffsets(string text)
        {
            if (string.IsNullOrEmpty(text)) return text;
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var sb = new StringBuilder();
            foreach (string line in lines)
            {
                if (IsHeaderLine(line)) continue;           // 헤더 줄 제외
                if (sb.Length > 0) sb.AppendLine();
                sb.Append(HasOffsetPrefix(line) ? line.Substring(OffsetLength) : line);
            }
            return sb.ToString().TrimEnd();
        }

        // 문자 인덱스로부터 데이터 바이트 인덱스 계산 (헤더 1줄 제외)
        public int GetByteIndexFromPosition(int charIndex)
        {
            int lineIndex     = GetLineFromCharIndex(charIndex);
            int dataLineIndex = Math.Max(0, lineIndex - 1); // 헤더 1줄 보정
            int lineStart     = GetFirstCharIndexFromLine(lineIndex);
            int posInLine     = charIndex - lineStart;

            if (posInLine < OffsetLength)
                return dataLineIndex * BytesPerLine;

            int hexPos = posInLine - OffsetLength;
            int half   = BytesPerLine / 2;

            int byteInLine = hexPos < half * 3
                ? hexPos / 3
                : Math.Max(0, (hexPos - 1) / 3);

            return dataLineIndex * BytesPerLine + Math.Min(byteInLine, BytesPerLine - 1);
        }
    }
}
