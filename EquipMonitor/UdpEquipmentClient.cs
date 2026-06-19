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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EquipMonitor
{
    class UdpEquipmentClient
    {
        public async Task RunAsync(EquipmentDto equipmentDto)
        {
            equipmentDto.initLogFile();
            EquipmentInfo info = equipmentDto.info;
            try
            {
                equipmentDto.ReceiveLogResponse("Client connected to server.");

                if (info.isHex)
                {
                    byte[] messageBytes = StringToByteArray(info.command.Replace(" ", ""));
                    equipmentDto.AddPacket(messageBytes, true);
                    await SendAndReceiveAsync(equipmentDto, info, messageBytes);
                }
                else
                {
                    string[] lines = info.command.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    foreach (string line in lines)
                    {
                        string command = line;
                        if (!string.IsNullOrEmpty(info.tail))
                        {
                            command += info.tail;
                            command = command.Replace("\\r", "\r")
                                             .Replace("\\n", "\n");
                        }

                        byte[] messageBytes = Encoding.UTF8.GetBytes(command);
                        equipmentDto.AddPacket(messageBytes, true);
                        await SendAndReceiveAsync(equipmentDto, info, messageBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                equipmentDto.ReceiveLogResponse(ex.Message);
            }
        }

        // 매 호출마다 새 UdpClient를 생성하여 이전 ReceiveAsync 태스크 누적 방지
        private async Task SendAndReceiveAsync(EquipmentDto equipmentDto, EquipmentInfo info, byte[] messageBytes)
        {
            using (UdpClient udpClient = new UdpClient())
            {
                await udpClient.SendAsync(messageBytes, messageBytes.Length, info.ip, info.port);

                var receiveTask = udpClient.ReceiveAsync();
                var timeoutTask = Task.Delay(3000);
                var completedTask = await Task.WhenAny(receiveTask, timeoutTask);

                if (completedTask == receiveTask)
                {
                    UdpReceiveResult result = await receiveTask;
                    equipmentDto.AddPacket(result.Buffer, false);
                }
                else
                {
                    equipmentDto.ReceiveLogResponse("장비 응답 시간이 초과되었습니다.");
                    // using 블록 종료 시 UdpClient가 Dispose되어 대기 중인 receiveTask도 종료됨
                }
            }
        }
        public static string ByteArrayToString(byte[] ba, int start, int size)
        {
            StringBuilder hex = new StringBuilder(size * 2);
            for (int i = start; i < size; i++)
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

        public static byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex = hex.Substring(0, hex.Length - 1);
            }

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
