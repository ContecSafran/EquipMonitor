using DotNetty.Buffers;
using DotNetty.Codecs;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using EquipMonitor.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EquipMonitor
{
    class TcpEquipmentClient
    {
        MultithreadEventLoopGroup group;
        IChannel channel;
        EquipmentDto equipmentDto;

        public async Task ConnectAsync(EquipmentDto equipmentDto)
        {
            this.equipmentDto = equipmentDto;
            EquipmentInfo info = equipmentDto.info;
            try
            {
                if (channel == null || !channel.Active)
                {
                    equipmentDto.initLogFile();
                    channel = await CreateAndConnectChannelAsync(info, equipmentDto);
                    equipmentDto.ReceiveLogResponse("Client connected to server. ip : " + info.ip + " port : " + info.port);
                    equipmentDto.OnConnectionStateChanged?.Invoke(true);
                }
            }
            catch (Exception ex)
            {
                equipmentDto.ReceiveLogResponse(ex.Message);
                await CloseAsync();
            }
        }

        private async Task<IChannel> CreateAndConnectChannelAsync(EquipmentInfo info, EquipmentDto equipmentDto)
        {
            group = new MultithreadEventLoopGroup();
            var bootstrap = new Bootstrap();
            bootstrap.Group(group)
                     .Channel<TcpSocketChannel>()
                     .Handler(new ActionChannelInitializer<ISocketChannel>(ctx =>
                     {
                         var pipeline = ctx.Pipeline;
                         pipeline.AddLast(new ClientHandler(equipmentDto));
                     }));
            return await bootstrap.ConnectAsync(new IPEndPoint(IPAddress.Parse(info.ip), info.port));
        }

        public async Task RunAsync(EquipmentDto equipmentDto)
        {
            EquipmentInfo info = equipmentDto.info;
            try
            {
                await ConnectAsync(equipmentDto);

                if (channel == null || !channel.Active) return;

                if (!string.IsNullOrEmpty(info.command))
                {
                    if (info.isHex)
                    {
                        byte[] messageBytes = StringToByteArray(info.command.Replace(" ", ""));
                        equipmentDto.AddPacket(messageBytes, true);
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
                        byte[] messageBytes = Encoding.UTF8.GetBytes(command);
                        equipmentDto.AddPacket(messageBytes, true);
                        await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(messageBytes));
                    }
                }
            }
            catch(Exception ex)
            {
                equipmentDto.ReceiveLogResponse(ex.Message);
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
                    channel = await CreateAndConnectChannelAsync(info, equipmentDto);
                    equipmentDto.ReceiveLogResponse("Client connected to server.");
                }

                equipmentDto.AddPacket(data, true);
                await channel.WriteAndFlushAsync(Unpooled.WrappedBuffer(data));
            }
            catch (Exception ex)
            {
                equipmentDto.ReceiveLogResponse(ex.Message);
                await CloseAsync();
            }
        }

        public async Task CloseAsync()
        {
            var ch = channel;
            var grp = group;
            channel = null;
            group = null;

            if (ch != null)
            {
                await ch.CloseAsync();
            }
            if (grp != null)
            {
                await grp.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1));
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
