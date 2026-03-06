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
                        equipmentDto.ReceiveResponse("send :" + info.command);
                        string tmp = "";
                        if (info.command.Contains("\\r\\n"))
                        {
                            tmp = info.command.Replace("\\", "");
                            byte[] messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 2] = 0x0d;
                            messageBytes[messageBytes.Length - 1] = 0x0a;
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes,0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                        }
                        else if (info.command.Contains("\\n"))
                        {
                            tmp = info.command.Replace("\\", "");
                            byte[] messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 1] = 0x0a;
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                        }
                        else if (info.command.Contains("\\r"))
                        {
                            tmp = info.command.Replace("\\", "");
                            byte[] messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 1] = 0x0d;
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                        }
                        else if (info.command.Contains("\\n\\r"))
                        {
                            tmp = info.command.Replace("\\", "");
                            byte[] messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 2] = 0x0a;
                            messageBytes[messageBytes.Length - 1] = 0x0d;
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                        }
                        else
                        {
                            byte[] messageBytes = Encoding.UTF8.GetBytes(tmp);
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                        }

                    }
                    await Task.Delay(5000);
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
