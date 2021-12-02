//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.IO;
//using System.Net.Sockets;
//using WeightCore.Helpers;

//namespace WeightCore.MassaK
//{
//    public class DeviceSocketTcpEntity
//    {
//        private ExceptionHelper _exception { get; set; } = ExceptionHelper.Instance;
//        public string DeviceIP { get; private set; }
//        public int DevicePort { get; private set; }
//        public int DeviceSendTimeout { get; set; }
//        public int DeviceReceiveTimeout { get; set; }
//        public TcpClient TcpClient { get; private set; }

//        public DeviceSocketTcpEntity(string ip, int port)
//        {
//            DeviceIP = ip;
//            DevicePort = port;
//        }

//        public byte[] Bytes(byte[] request)
//        {
//            byte[] result = null;
//            try
//            {
//                TcpClient = new TcpClient(DeviceIP, DevicePort)
//                {
//                    SendTimeout = DeviceSendTimeout,
//                    ReceiveTimeout = DeviceReceiveTimeout,
//                };

//                NetworkStream stream = TcpClient.GetStream();
//                stream.Write(request, 0, request.Length);

//                using (MemoryStream ms = new())
//                {
//                    byte[] readingData = new byte[256];
//                    int numberOfBytesRead = 0;
//                    do
//                    {
//                        numberOfBytesRead = stream.Read(readingData, 0, readingData.Length);
//                        foreach (byte b in readingData)
//                        {
//                            ms.Write(readingData, 0, readingData.Length);
//                        }
//                    }
//                    while (stream.DataAvailable);
//                    result = ms.ToArray();
//                }

//                stream.Close();
//                TcpClient.Close();
//            }
//            catch (Exception ex)
//            {
//                _exception.Catch(null, ref ex);
//            }
//            return result;
//        }

//        ~DeviceSocketTcpEntity()
//        {
//            TcpClient?.Close();
//        }
//    }
//}
