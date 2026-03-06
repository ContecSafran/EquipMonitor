using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using FatClient.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatClient
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
            string receivedMessage = ByteArrayToString(msg.Array, msg.ArrayOffset, msg.ReadableBytes);
            equipmentDto.ReceiveResponse(receivedMessage);
        }
        public static string ByteArrayToString(byte[] ba, int start, int size)
        {
            byte[] buffer = new byte[size];
            Buffer.BlockCopy(ba, start, buffer, 0, size);

            return BitConverter.ToString(buffer).Replace("-", " ");
        }
    }
}
