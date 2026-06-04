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
        MultithreadEventLoopGroup group;
        IChannel channel;

        public async Task ConnectAsync(EquipmentDto equipmentDto)
        {
            EquipmentInfo info = equipmentDto.info;
            try
            {
                if (channel == null || !channel.Active)
                {
                    equipmentDto.initLogFile();
                    group = new MultithreadEventLoopGroup();
                    var bootstrap = new Bootstrap();
                    bootstrap.Group(group)
                             .Channel<TcpSocketChannel>()
                             .Handler(new ActionChannelInitializer<ISocketChannel>(ctx =>
                             {
                                 var pipeline = ctx.Pipeline;
                                 if (info.isHex)
                                 {
                                     pipeline.AddLast(new LengthFieldBasedFrameDecoder(100000, 4, 4, -8, 0));
                                     pipeline.AddLast(new SafranByteToMessageDecoder(equipmentDto));
                                 }
                                 else
                                 {
                                     pipeline.AddLast(new ClientHandler(equipmentDto));
                                 }
                             }));

                    channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(info.ip), info.port));
                    equipmentDto.ReceiveAsciiResponse("Client connected to server. ip : " + info.ip + " port : " + info.port);
                }
            }
            catch (Exception ex)
            {
                equipmentDto.ReceiveHexResponse(ex.Message);
                await CloseAsync();
            }
        }

        public async Task RunAsync(EquipmentDto equipmentDto)
        {
            EquipmentInfo info = equipmentDto.info;
            try
            {
                await ConnectAsync(equipmentDto);

                if (!string.IsNullOrEmpty(info.command))
                {
                    if (info.isHex)
                    {
                        byte[] messageBytes = StringToByteArray(info.command.Replace(" ", ""));
                        equipmentDto.ReceiveHexResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                        await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                    }
                    else
                    {
                        string command = info.command;
                        if (!string.IsNullOrEmpty(info.tail))
                        {
                            command += info.tail;
                            command = command.Replace("\\r", "\r")
                                             .Replace("\\n", "\n");
                        }
                        equipmentDto.ReceiveAsciiResponse("send :" + command);
                        byte[] messageBytes = Encoding.UTF8.GetBytes(command);
                        equipmentDto.ReceiveHexResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                        await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                    }
                }
            }
            catch(Exception ex)
            {
                equipmentDto.ReceiveHexResponse(ex.Message);
                await CloseAsync();
            }
        }

        public async Task RunAsyncByBuffer(EquipmentDto equipmentDto, byte [] data)
        {
            EquipmentInfo info = equipmentDto.info;
            try
            {
                if (channel == null || !channel.Active)
                {
                    equipmentDto.initLogFile();
                    group = new MultithreadEventLoopGroup();
                    var bootstrap = new Bootstrap();
                    bootstrap.Group(group)
                             .Channel<TcpSocketChannel>()
                             .Handler(new ActionChannelInitializer<ISocketChannel>(ctx =>
                             {
                                 var pipeline = ctx.Pipeline;
                                 pipeline.AddLast(new LengthFieldBasedFrameDecoder(100000, 4, 4, -8, 0));
                                 pipeline.AddLast(new SafranByteToMessageDecoder(equipmentDto));
                             }));

                    channel = await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(info.ip), info.port));
                    equipmentDto.ReceiveHexResponse("Client connected to server.");
                }

                equipmentDto.ReceiveHexResponse("hex 전송 size : " + data.Length.ToString());
                await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(data));
            }
            catch (Exception ex)
            {
                equipmentDto.ReceiveHexResponse(ex.Message);
                await CloseAsync();
            }
        }

        public async Task CloseAsync()
        {
            if (channel != null)
            {
                await channel.CloseAsync();
                channel = null;
            }
            if (group != null)
            {
                await group.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
                group = null;
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
