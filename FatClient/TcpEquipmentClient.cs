using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using FatClient.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FatClient
{
    class TcpEquipmentClient
    {
        public async Task RunAsync(EquipmentDto equipmentDto)
        {
            var group = new MultithreadEventLoopGroup();
            equipmentDto.initLogFile();
            EquipmentInfo info = equipmentDto.info;
            try
            {

                if (info.isHex)
                {
                    var bootstrap = new Bootstrap();
                    bootstrap.Group(group)
                             .Channel<TcpSocketChannel>()
                             .Handler(new ActionChannelInitializer<ISocketChannel>(ctx =>
                             {
                                 var pipeline = ctx.Pipeline;
                                 pipeline.AddLast(new LengthFieldBasedFrameDecoder(100000, 4, 4, -8, 0));
                                 pipeline.AddLast(new SafranByteToMessageDecoder(equipmentDto));
                             }));

                    IChannel channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(info.ip), info.port));
                    equipmentDto.ReceiveResponse("Client connected to server.");
                    byte[] messageBytes = StringToByteArray(info.command.Replace(" ", ""));
                    equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                    await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                    await Task.Delay(5000);
                    await channel.CloseAsync();
                }
                else
                {
                    var bootstrap = new Bootstrap();
                    bootstrap.Group(group)
                             .Channel<TcpSocketChannel>()
                             .Handler(new ActionChannelInitializer<ISocketChannel>(ctx =>
                             {
                                 var pipeline = ctx.Pipeline;
                                 pipeline.AddLast(new ClientHandler(equipmentDto));
                             }));

                    IChannel channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(info.ip), info.port));
                    equipmentDto.ReceiveResponse("Client connected to server. ip : " + info.ip + " port : " + info.port);
                    if (info.command != "")
                    {
                        string[] lines = info.command.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                        foreach (string line in lines)
                        {
                            string command = line;
                            if (!string.IsNullOrEmpty(info.tail))
                            {
                                command += info.tail;
                                command = command.Replace("\\r", "\r")  // 텍스트 "\r"을 실제 0x0D로
                                                 .Replace("\\n", "\n"); // 텍스트 "\n"을 실제 0x0A로
                            }
                            equipmentDto.ReceiveResponse("send :" + command);
                            byte[] messageBytes = Encoding.UTF8.GetBytes(command);
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                            await Task.Delay(info.timeOut);
                        }
                    }
                    else
                    {
                        await Task.Delay(info.timeOut);
                    }
                    await channel.CloseAsync();
                }
                equipmentDto.ReceiveResponse("close");

            }
            catch(Exception ex)
            {
                equipmentDto.ReceiveResponse(ex.Message);
            }
            finally
            {
                await group.ShutdownGracefullyAsync();
            }
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
