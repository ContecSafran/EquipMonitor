using System;

namespace EquipMonitor
{
    public enum PacketDirection { Send, Recv }

    public static class PacketCaptureHub
    {
        public static event Action<PacketDirection, string, int, string, int, byte[]> OnPacket;

        public static void Publish(PacketDirection dir,
            string localIP, int localPort,
            string remoteIP, int remotePort,
            byte[] data)
        {
            OnPacket?.Invoke(dir, localIP, localPort, remoteIP, remotePort, data);
        }
    }
}
