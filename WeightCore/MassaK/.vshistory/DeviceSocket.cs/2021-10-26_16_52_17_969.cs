// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.Helpers;
using System;
using System.IO;
using System.IO.Ports;
using System.Net.Sockets;
using System.Threading;

namespace WeightCore.MassaK
{
    public abstract class DeviceSocketEntity
    {
        public abstract byte[] Bytes(byte[] request);
    }

    public class DeviceSocketRs232 : DeviceSocketEntity
    {
        public SerialPort SerialPortItem { get; private set; }
        private readonly LogHelper _log = LogHelper.Instance;

        public DeviceSocketRs232(string portName)
        {
            SerialPortItem = SerialPortItem.GetDefault(portName);
        }

        ~DeviceSocketRs232()
        {
            SerialPortItem?.Close();
        }

        public override byte[] Bytes(byte[] request)
        {
            byte[] result = null;
            try
            {
                if (!SerialPortItem.IsOpen)
                {
                    SerialPortItem.Open();
                    Thread.Sleep(50);
                }

                if (SerialPortItem.IsOpen)
                {
                    SerialPortItem.Write(request, 0, request.Length);
                    Thread.Sleep(50);
                    int bytes = SerialPortItem.BytesToRead;
                    byte[] buffer = new byte[bytes];
                    if (bytes > 0)
                    {
                        SerialPortItem.Read(buffer, 0, bytes);
                        result = buffer;
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Error(nameof(DeviceSocketRs232), nameof(Bytes), ex.Message);
            }
            return result;
        }
    }

    public class DeviceSocketTcp : DeviceSocketEntity
    {
        private readonly LogHelper _log = LogHelper.Instance;
        public string DeviceIP { get; private set; }
        public int DevicePort { get; private set; }
        public int DeviceSendTimeout { get; set; }
        public int DeviceReceiveTimeout { get; set; }
        public TcpClient TcpClient { get; private set; }

        public DeviceSocketTcp(string ip, int port)
        {
            DeviceIP = ip;
            DevicePort = port;
        }

        [Obsolete(@"Сделать по аналогии с RS232")]
        public override byte[] Bytes(byte[] request)
        {
            byte[] result = null;
            try
            {
                TcpClient = new TcpClient(DeviceIP, DevicePort)
                {
                    SendTimeout = DeviceSendTimeout,
                    ReceiveTimeout = DeviceReceiveTimeout,
                };

                NetworkStream stream = TcpClient.GetStream();
                stream.Write(request, 0, request.Length);

                using (MemoryStream ms = new())
                {
                    byte[] readingData = new byte[256];
                    int numberOfBytesRead = 0;
                    do
                    {
                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
                        foreach (byte b in readingData)
                        {
                            ms.Write(readingData, 0, readingData.Length);
                        }
                    }
                    while (stream.DataAvailable);
                    result = ms.ToArray();
                }

                stream.Close();
                TcpClient.Close();
            }
            catch (ArgumentNullException ex)
            {
                _log.Error(nameof(DeviceSocketEntity), nameof(Bytes), ex.Message);
            }
            catch (SocketException ex)
            {
                _log.Error(nameof(DeviceSocketEntity), nameof(Bytes), ex.Message);
            }
            return result;
        }

        ~DeviceSocketTcp()
        {
            TcpClient?.Close();
        }
    }
}
