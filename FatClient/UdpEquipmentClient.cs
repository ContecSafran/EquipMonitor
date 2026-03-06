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
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FatClient
{
    class UdpEquipmentClient
    {
        public async Task RunAsync(EquipmentDto equipmentDto)
        {
            equipmentDto.initLogFile();
            EquipmentInfo info = equipmentDto.info;
            using (UdpClient udpClient = new UdpClient())
            {
                try
                {
                    equipmentDto.ReceiveResponse("Client connected to server.");

                    equipmentDto.ReceiveResponse("send :" + info.command);
                    // 1. 데이터 변환 및 전송
                    if (info.isHex)
                    {
                        byte[] messageBytes = StringToByteArray(info.command.Replace(" ", ""));
                        await udpClient.SendAsync(messageBytes, messageBytes.Length, info.ip, info.port);
                    }
                    else
                    {
                        string tmp = "";
                        byte[] messageBytes = null;
                        if (info.command.Contains("\\r\\n"))
                        {
                            tmp = info.command.Replace("\\", "");
                            messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 2] = 0x0d;
                            messageBytes[messageBytes.Length - 1] = 0x0a;
                        }
                        else if (info.command.Contains("\\n"))
                        {
                            tmp = info.command.Replace("\\", "");
                            messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 1] = 0x0a;
                        }
                        else if (info.command.Contains("\\r"))
                        {
                            tmp = info.command.Replace("\\", "");
                            messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 1] = 0x0d;
                        }
                        else if (info.command.Contains("\\n\\r"))
                        {
                            tmp = info.command.Replace("\\", "");
                            messageBytes = Encoding.UTF8.GetBytes(tmp);
                            messageBytes[messageBytes.Length - 2] = 0x0a;
                            messageBytes[messageBytes.Length - 1] = 0x0d;
                        }
                        if (messageBytes != null)
                        {
                            equipmentDto.ReceiveResponse("hex :" + SafranByteToMessageDecoder.ByteArrayToString(messageBytes, 0, messageBytes.Length));
                            await udpClient.SendAsync(messageBytes, messageBytes.Length, info.ip, info.port);
                        }
                        else
                        {
                            equipmentDto.ReceiveResponse("요청 메시지가 정상적이지 않습니다.");
                            return;
                        }
                    }
                    // 2. 응답 대기 (타임아웃 처리)
                    // Task.WhenAny를 사용하여 수신과 타임아웃 중 먼저 끝나는 쪽을 처리
                    var receiveTask = udpClient.ReceiveAsync();
                    var timeoutTask = Task.Delay(5000);

                    var completedTask = await Task.WhenAny(receiveTask, timeoutTask);

                    if (completedTask == receiveTask)
                    {
                        // 응답 성공
                        UdpReceiveResult result = await receiveTask;

                        equipmentDto.ReceiveResponse("receive hex :" + SafranByteToMessageDecoder.ByteArrayToString(result.Buffer, 0, result.Buffer.Length));
                        if (info.isHex)
                        {
                            string receivedMessage = ByteArrayToString(result.Buffer, 0, result.Buffer.Length);
                            equipmentDto.ReceiveResponse(receivedMessage);

                        }
                        else
                        {
                            string receivedMessage = Encoding.UTF8.GetString(result.Buffer);
                            equipmentDto.ReceiveResponse(receivedMessage);
                        }
                    }
                    else
                    {
                        // 타임아웃 발생
                        equipmentDto.ReceiveResponse("장비 응답 시간이 초과되었습니다.");
                    }
                }
                catch (Exception ex)
                {
                    equipmentDto.ReceiveResponse(ex.Message);
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
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
