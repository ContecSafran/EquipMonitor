using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using FatClient.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatClient
{
    class ClientHandler : SimpleChannelInboundHandler<IByteBuffer>
    {
        EquipmentDto equipmentDto;
        public ClientHandler(EquipmentDto equipmentDto)
        {
            this.equipmentDto = equipmentDto;
        }
        protected override void ChannelRead0(IChannelHandlerContext ctx, IByteBuffer msg)
        {
            if (this.equipmentDto.info.isHex)
            {
                string receivedMessage = ByteArrayToString(msg.Array, 0, msg.ReadableBytes);
                equipmentDto.ReceiveResponse("receive : " + receivedMessage);


            }
            else
            {
                string receivedMessage = msg.ToString(Encoding.UTF8);
                equipmentDto.ReceiveResponse("receive : " + receivedMessage);
                byte[] messageBytes = Encoding.UTF8.GetBytes(receivedMessage);
                equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
            }
        }
        public static string ByteArrayToString(byte[] ba, int start, int size)
        {
            StringBuilder hex = new StringBuilder(size * 2);
            for(int i = start; i < size; i++)
            {
                if (i == start)
                {
                    hex.AppendFormat("{0:x2}", ba[i]);
                }
                else
                {
                    hex.AppendFormat(" {0:x2}", ba[i]);
                }
            }
            return hex.ToString();
        }
        public override void ExceptionCaught(IChannelHandlerContext context, Exception exception)
        {
            equipmentDto.ReceiveResponse(exception.Message);
            context.CloseAsync();
        }
    }
}
