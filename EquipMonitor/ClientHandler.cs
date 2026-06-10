using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using EquipMonitor.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipMonitor
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
                equipmentDto.ReceiveHexResponse("receive : " + receivedMessage);


            }
            else
            {
                string receivedMessage = msg.ToString(Encoding.UTF8);
                equipmentDto.ReceiveAsciiResponse("receive : " + receivedMessage);
                byte[] messageBytes = Encoding.UTF8.GetBytes(receivedMessage);
                equipmentDto.ReceiveHexResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
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
            equipmentDto.ReceiveHexResponse(exception.Message);
            context.CloseAsync();
        }
        public override void ChannelActive(IChannelHandlerContext context)
        {
            base.ChannelActive(context);
            equipmentDto.OnConnectionStateChanged?.Invoke(true);
        }
        public override void ChannelInactive(IChannelHandlerContext context)
        {
            base.ChannelInactive(context);
            equipmentDto.OnConnectionStateChanged?.Invoke(false);
        }
    }
}
