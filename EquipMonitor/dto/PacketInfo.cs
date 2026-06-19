using System;

namespace EquipMonitor.dto
{
    public class PacketInfo
    {
        public DateTime Time { get; set; }
        public bool IsSend { get; set; }
        public byte[] Data { get; set; }

        public override string ToString()
        {
            string direction = IsSend ? "TX" : "RX";
            return $"[{Time:HH:mm:ss.fff}] [{direction}] {Data.Length}bytes";
        }
    }
}
