//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using System;
//using System.IO.Ports;
//using System.Threading;
//using WeightCore.Helpers;

//namespace WeightCore.MassaK
//{
//    public class DeviceSocketComEntity
//    {
//        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
//        public SerialPort SerialPortItem { get; private set; }
//        public DeviceSocketComEntity(string portName, int readTimeout, int writeTimeout)
//        {
//            SerialPortItem = SerialPortItem.GetDefault(portName, readTimeout, writeTimeout);
//        }

//        ~DeviceSocketComEntity()
//        {
//            SerialPortItem?.Close();
//        }

//        public byte[] Bytes(byte[] request)
//        {
//            byte[] result = null;
//            try
//            {
//                if (!SerialPortItem.IsOpen)
//                {
//                    SerialPortItem.Open();
//                    Thread.Sleep(50);
//                }

//                if (SerialPortItem.IsOpen)
//                {
//                    SerialPortItem.Write(request, 0, request.Length);
//                    Thread.Sleep(50);
//                    int bytes = SerialPortItem.BytesToRead;
//                    byte[] buffer = new byte[bytes];
//                    if (bytes > 0)
//                    {
//                        SerialPortItem.Read(buffer, 0, bytes);
//                        result = buffer;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                _exception.Catch(null, ref ex);
//            }
//            return result;
//        }
//    }
//}
