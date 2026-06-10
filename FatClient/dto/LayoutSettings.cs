using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FatClient.dto
{
    public class LayoutSettings
    {
        public int FormWidth { get; set; } = 960;
        public int FormHeight { get; set; } = 540;
        public FormWindowState FormState { get; set; } = FormWindowState.Normal;
        public Dictionary<string, int> EquipmentSplitterDistances { get; set; } = new Dictionary<string, int>();
    }
}
