using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using EquipMonitor.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMonitor
{
    public class SafranByteToMessageDecoder : ByteToMessageDecoder
    {
        EquipmentDto equipmentDto;
        public SafranByteToMessageDecoder(EquipmentDto equipmentDto)
        {
            this.equipmentDto = equipmentDto;
        }
        protected override void Decode(IChannelHandlerContext context, IByteBuffer msg, List<object> output)
        {
            string receivedMessage = ByteArrayToString(msg.Array, msg.ArrayOffset + msg.ReaderIndex, msg.ReadableBytes);
            equipmentDto.ReceiveHexResponse(receivedMessage);
        }
        public static string ByteArrayToString(byte[] ba, int start, int size)
        {
            byte[] buffer = new byte[size];
            Buffer.BlockCopy(ba, start, buffer, 0, size);

            return BitConverter.ToString(buffer).Replace("-", " ");
        }
    }
}
