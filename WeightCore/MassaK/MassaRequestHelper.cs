// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
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

        #region Public and private fields and properties

        private BytesHelper Bytes { get; set; } = BytesHelper.Instance;

        public readonly byte[] Header = { 0xF8, 0x55, 0xCE };

        #endregion

        #region Public and private methods

        public byte[] MakeRequestCrcAdd(byte[] body)
        {
            byte[] len = BitConverter.GetBytes((ushort)body.Length);
            byte[] crc = Bytes.CrcGet(body);
            return Bytes.MergeBytes(new List<byte[]>() { Header, len, body, crc });
        }

        public byte[] MakeRequestCrcAdd(byte body) => MakeRequestCrcAdd(new byte[] { body });

        public byte[] MakeRequestCrcRecalc(byte[] request)
        {
            if (request.Length < 8)
                throw new ArgumentException($"Length of {nameof(request)} must be more than 8 digits!");
            if (request[0] != Header[0] || request[1] != Header[1] || request[2] != Header[2])
                throw new ArgumentException($"{nameof(Header)} must be '{Bytes.GetBytesAsHex(Header, ' ')}'!");
            byte[] len = new byte[2];
            len[0] = request[3];
            len[1] = request[4];
            ushort lenAsUshort = BitConverter.ToUInt16(len, 0);
            byte[] body = request.Skip(5).Take(lenAsUshort).ToArray();
            return Bytes.MergeBytes(new List<byte[]>() { Header, len, Bytes.CrcGetWithBody(body) });
        }

        #endregion

        #region Public and private methods - API

        /// <summary>
        /// Неизвестная команда.
        /// F8 55 CE 01 00 F0 F0 00
        /// </summary>
        public byte[] CMD_NACK => MakeRequestCrcAdd(0xF0);

        /// <summary>
        /// Запрос о наличии подключенных терминалов.
        /// F8 55 CE 01 00 00 00 00
        /// </summary>
        public byte[] CMD_UDP_POLL => MakeRequestCrcAdd(0x00);

        /// <summary>
        /// F8 55 CE 01 00 29 29 00
        /// </summary>
        public byte[] CMD_GET_INIT_2 => MakeRequestCrcAdd(0x29);

        /// <summary>
        /// F8 55 CE 01 00 20 20 00
        /// </summary>
        public byte[] CMD_GET_INIT_3 => MakeRequestCrcAdd(0x20);

        /// <summary>
        /// Запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары.
        /// F8 55 CE 01 00 23 23 00
        /// </summary>
        public byte[] CMD_GET_MASSA => MakeRequestCrcAdd(0x23);

        /// <summary>
        /// Запрос параметров Ethernet.
        /// F8 55 CE 01 00 2D 2D 00
        /// </summary>
        public byte[] CMD_GET_ETHERNET => MakeRequestCrcAdd(0x2D);

        /// <summary>
        /// Запрос параметров Wi-Fi.
        /// F8 55 CE 01 00 33 33 00
        /// </summary>
        public byte[] CMD_GET_WIFI_IP => MakeRequestCrcAdd(0x33);

        /// <summary>
        /// Запрос параметров доступа к Wi-Fi.
        /// F8 55 CE 01 00 3A 3A 00
        /// </summary>
        public byte[] CMD_GET_WIFI_SSID => MakeRequestCrcAdd(0x3A);

        /// <summary>
        /// Запрос параметров весового устройства.
        /// F8 55 CE 01 00 75 75 00
        /// </summary>
        public byte[] CMD_GET_SCALE_PAR => MakeRequestCrcAdd(0x75);

        /// <summary>
        /// F8 55 CE 01 00 25 25 00
        /// </summary>
        public byte[] CMD_GET_SCALE_PAR_AFTER => MakeRequestCrcAdd(0x25);

        /// <summary>
        /// Запрос идентификатора весов.
        /// F8 55 CE 01 00 90 90 00
        /// </summary>
        public byte[] CMD_GET_ID => MakeRequestCrcAdd(0x90);

        /// <summary>
        /// F8 55 CE 01 00 20 20 00
        /// </summary>
        public byte[] CMD_GET_NAME => MakeRequestCrcAdd(0x20);

        /// <summary>
        /// F8 55 CE 01 00 A1 00 00
        /// </summary>
        public byte[] CMD_GET_TARE => MakeRequestCrcAdd(0xA1);

        /// <summary>
        /// F8 55 CE 01 00 A0 A0 00
        /// </summary>
        public byte[] CMD_GET_WEIGHT => MakeRequestCrcAdd(0xA0);

        /// <summary>
        /// F8 55 CE 01 00 72 72 00
        /// </summary>
        public byte[] CMD_SET_ZERO => MakeRequestCrcAdd(0x72);

        /// <summary>
        /// F8 55 CE 01 00 22 22 00
        /// </summary>
        public byte[] CMD_SET_NAME => MakeRequestCrcAdd(0x22);

        /// <summary>
        /// F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
        /// </summary>
        //public byte[] CMD_GET_SYS => MakePacketCrcAdd(new byte[] { 0x08, 0x00 }, new byte[] { 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF });
        public byte[] CMD_GET_SYS => MakeRequestCrcAdd(new byte[] { 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF });

        /// <summary>
        /// F8 55 CE 05 00 A3 00 00 00 00
        /// </summary>
        public byte[] CMD_SET_TARE => MakeRequestCrcAdd(new byte[] { 0xA3, 0x00, 0x00, 0x00, 0x00 });

        ///// <summary>
        ///// F8 55 CE 06 00 55 07 2C 01 00 00 44 1F
        ///// </summary>
        //public byte[] CMD_SET_RGNUM => MakePacketCrcAdd(new byte[] { 0x06, 0x00 }, new byte[] { 0x55, 0x07, 0x00, 0x00, 0x00, 0x00 });

        ///// <summary>
        ///// F8 55 CE 08 00 55 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>, где 13 0C 0B 00 00 00 - дата/время (00:00:00 11.12.19)
        ///// </summary>
        //public byte[] CMD_SET_DATETIME => MakePacketCrcAdd(new byte[] { 0x08, 0x00 }, new byte[] { 0x55, 0x01, 0x00, 0x00, 0x00, 0x00 });

        #endregion
    }
}
