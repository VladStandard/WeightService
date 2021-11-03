// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace WeightCore.MassaK
{
    public class MassaRequestHelper
    {
        #region Design pattern "Lazy Singleton"

        private static MassaRequestHelper _instance;
        public static MassaRequestHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        private Crc16MassaEntity Crc16Massa = Crc16MassaEntity.Instance;

        public string GetBytesAsHex(byte[] bytes) => string.Join(", ", bytes.Select(b => b.ToString("X2")));
        
        private readonly byte[] _cmdStart = { 0xF8, 0x55, 0xCE };

        private byte[] MakePacket(List<byte[]> bytesList)
        {
            int len = 0;
            foreach (byte[] bytes in bytesList)
            {
                len += bytes.Length;
            }
            byte[] data = new byte[len];

            int i = 0;
            foreach (byte[] bytes in bytesList)
            {
                foreach (byte item in bytes)
                {
                    data[i] = item;
                    i++;
                }
            }
            return data;
        }

        public byte[] CrcAdd(byte[] body)
        {
            ushort crc = Crc16Massa.ComputeChecksum(body);
            byte[] crcBytes = new byte[2];
            crcBytes[0] = (byte)crc;
            crcBytes[1] = (byte)(crc >> 8);
            //response[response.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            //response[response.Length - 1] = (byte)(crc & 0xFF);
            return MakePacket(new List<byte[]>() { body, crcBytes });
        }

        public byte[] CrcRecalc(byte[] body)
        {
            ushort crc = Crc16Massa.ComputeChecksum(body.Take(body.Length - 2).ToArray());
            body[body.Length - 2] = (byte)crc;
            body[body.Length - 1] = (byte)(crc >> 8);
            //response[response.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            //response[response.Length - 1] = (byte)(crc & 0xFF);
            return body;
        }

        private byte[] MakePacketCrcAdd(byte[] len, byte[] body)
        {
            CrcAdd(body);
            return MakePacket(new List<byte[]>() { _cmdStart, len, body });
        }

        private byte[] MakePacketCrcRecalc(byte[] body)
        {
            CrcRecalc(body);
            return MakePacket(new List<byte[]>() { _cmdStart, body });
        }

        /// <summary>
        /// Неизвестная команда.
        /// F8 55 CE 01 00 F0 F0 00
        /// </summary>
        public byte[] CMD_NACK => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0xF0, 0xF0, 0x00 });

        /// <summary>
        /// Запрос параметров весов.
        /// F8 55 CE 01 00 00 00 00
        /// </summary>
        public byte[] CMD_GET_INIT_1 => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 });

        /// <summary>
        /// F8 55 CE 01 00 29 29 00
        /// </summary>
        public byte[] CMD_GET_INIT_2 => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x29, 0x29, 0x00 });

        /// <summary>
        /// F8 55 CE 01 00 20 20 00
        /// </summary>
        public byte[] CMD_GET_INIT_3 => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x20, 0x20, 0x00 });

        /// <summary>
        /// Запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары.
        /// F8 55 CE 01 00 23 23 00
        /// </summary>
        public byte[] CMD_GET_MASSA => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x23 });

        /// <summary>
        /// Запрос параметров Ethernet.
        /// F8 55 CE 01 00 2D 2D 00
        /// </summary>
        public byte[] CMD_GET_ETHERNET => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x2D, 0x2D, 0x00 });

        /// <summary>
        /// Запрос параметров Wi-Fi.
        /// F8 55 CE 01 00 33 33 00
        /// </summary>
        public byte[] CMD_GET_WIFI_IP => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x33, 0x33, 0x00 });

        /// <summary>
        /// Запрос параметров доступа к Wi-Fi.
        /// F8 55 CE 01 00 3A 3A 00
        /// </summary>
        public byte[] CMD_GET_WIFI_SSID => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x3A, 0x3A, 0x00 });

        /// <summary>
        /// Запрос параметров весового устройства.
        /// F8 55 CE 01 00 75 75 00
        /// </summary>
        public byte[] CMD_GET_SCALE_PAR => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x75, 0x75, 0x00 });

        /// <summary>
        /// F8 55 CE 01 00 25 25 00
        /// </summary>
        public byte[] CMD_GET_SCALE_PAR_AFTER => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x25, 0x25, 0x00 });

        /// <summary>
        /// Запрос идентификатора весов.
        /// F8 55 CE 01 00 90 90 00
        /// </summary>
        public byte[] CMD_GET_ID => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x90, 0x90, 0x00 });

        /// <summary>
        /// F8 55 CE, 0x01, 0x00, 0x20, 0x00, 0x00
        /// </summary>
        /// public byte[] CMD_TCP_GET_NAME = {  };
        public byte[] CMD_GET_NAME => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x20 });

        /// <summary>
        /// F8 55 CE 01 00 A1 00 00
        /// </summary>
        public byte[] CMD_GET_TARE => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0xA1 });

        /// <summary>
        /// F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
        /// </summary>
        public byte[] CMD_GET_SYS => MakePacketCrcAdd(new byte[] { 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF });

        /// <summary>
        /// F8 55 CE 01 00 A0 00 00
        /// </summary>
        public byte[] CMD_GET_WEIGHT => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0xA0 });

        /// <summary>
        /// 
        /// </summary>
        public byte[] CMD_SET_TARE => MakePacketCrcAdd(new byte[] { 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00 });

        /// <summary>
        /// F8 55 CE 01 00 72 72 00
        /// </summary>
        public byte[] CMD_SET_ZERO => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x72, 0x72, 0x00 });

        /// <summary>
        /// F8 55 CE 06 00 55 07 2C 01 00 00 44 1F
        /// </summary>
        public byte[] CMD_SET_RGNUM => MakePacketCrcAdd(new byte[] { 0x06, 0x00, 0x55, 0x07, 0x00, 0x00, 0x00, 0x00 });

        /// <summary>
        /// F8 55 CE 08 00 55 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>, где 13 0C 0B 00 00 00 - дата/время (00:00:00 11.12.19)
        public byte[] CMD_SET_DATETIME => MakePacketCrcAdd(new byte[] { 0x08, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0x00 });

        /// <summary>
        /// F8 55 CE 01 00 22 22 00
        /// </summary>
        public byte[] CMD_SET_NAME => MakePacketCrcAdd(new byte[] { 0x01, 0x00, 0x22 });
    }
}
