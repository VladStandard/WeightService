// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// Protokol_100 (r 5) 2018 V3.pdf

using System.Collections.Generic;

namespace WeightCore.MassaK
{
    public static class MassaUtils
    {
        public static class Cmd
        {
            /// <summary>
            /// Packet's start.
            /// </summary>
            private static readonly byte[] CMD_BEGIN = { 0xF8, 0x55, 0xCE, };

            /// <summary>
            /// Packet's end.
            /// </summary>
            private static readonly byte[] CMD_END = { 0x00, 0x00, };

            private static byte[] GetNew(List<byte[]> bytesList)
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

            private static byte[] GetFull(byte[] bytes, bool addEnd = true) => addEnd
                ? GetNew(new List<byte[]>() { CMD_BEGIN, bytes, CMD_END })
                : GetNew(new List<byte[]>() { CMD_BEGIN, bytes });

            public static class Get
            {
                /// <summary>
                /// Запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары.
                /// </summary>
                /// public static readonly byte[] CMD_GET_MASSA = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x23, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_MASSA = GetFull(new byte[] { 0x01, 0x00, 0x23 });

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0001 длина тела сообщения
                /// byte Command 0x20 CMD_GET_NAME
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x20, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_NAME = GetFull(new byte[] { 0x01, 0x00, 0x20 });

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0001 длина тела сообщения
                /// byte Command 0xA1 CMD_TCP_GET_TARE
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_TARE = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA1, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_TARE = GetFull(new byte[] { 0x01, 0x00, 0xA1 });

                /// <summary>
                /// Прочитать служебную информацию.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_SYS = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF };
                public static readonly byte[] CMD_GET_SYS = GetFull(new byte[] { 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF }, false);

                /// <summary>
                /// Запрос текущей массы, цены деления и признака стабильности показаний.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0001 длина тела сообщения
                /// byte Command 0xA0 CMD_TCP_GET_WEIGHT
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_WEIGHT = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA0, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_WEIGHT = GetFull(new byte[] { 0x01, 0x00, 0xA0 });

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0001 длина тела сообщения
                /// byte Command 0x75 CMD_GET_SCALE_PAR
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_GET_SCALE_PAR = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x75, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_SCALE_PAR = GetFull(new byte[] { 0x01, 0x00, 0x75 });
            }

            public static class Set
            {
                /// <summary>
                /// Установить тару.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0005 длина тела сообщения
                /// byte Command 0xA3 CMD_TCP_SET_TARE
                /// int Tare 4 байта Масса тары в делениях
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_TCP_SET_TARE = { 0xF8, 0x55, 0xCE, 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                public static readonly byte[] CMD_SET_TARE = GetFull(new byte[] { 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00 });

                /// <summary>
                /// В ряде весовых устройств команда не поддерживается (весовое устройство отвечает командой «CMD_NACK»).
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// int16 Len 0x0001 длина тела сообщения
                /// byte Command 0x72 Код команды CMD_SET_ZERO
                /// int16 CRC 2 байта CRC (см. Приложение 7.1)
                /// </summary>
                /// public static readonly byte[] CMD_TCP_SET_ZERO = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x72, 0x00, 0x00 };
                public static readonly byte[] CMD_SET_ZERO = GetFull(new byte[] { 0x01, 0x00, 0x72 });

                /// <summary>
                /// Сохранить номер регистрации.
                /// F8 55 CE 06 00 55 07 2C 01 00 00 44 1F
                /// </summary>
                /// public static readonly byte[] CMD_TCP_SET_RGNUM = { 0xF8, 0x55, 0xCE, 0x06, 0x00, 0x55, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                public static readonly byte[] CMD_SET_RGNUM = GetFull(new byte[] { 0x06, 0x00, 0x55, 0x07, 0x00, 0x00, 0x00, 0x00 });

                /// <summary>
                /// Установить дату/время.
                /// Запрос: F8 55 CE 08 00 55 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>, где 13 0C 0B 00 00 00 - дата/время (00:00:00 11.12.19)
                /// Ответ(всё ОК): F8 55 CE 06 00 56 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>
                /// </summary>
                /// public static readonly byte[] CMD_TCP_SET_DATETIME = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                public static readonly byte[] CMD_SET_DATETIME = GetFull(new byte[] { 0x08, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0x00 });

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// byte Header[0] 0xF8 заголовочная последовательность
                /// byte Header[1] 0x55 заголовочная последовательность
                /// byte Header[2] 0xCE заголовочная последовательность
                /// word Len 0x0001 длина тела сообщения
                /// byte Command 0x22 CMD_SET_NAME
                /// word CRC 2 байта CRC
                /// </summary>
                /// public static readonly byte[] CMD_SET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x22 };
                public static readonly byte[] CMD_SET_NAME = GetFull(new byte[] { 0x01, 0x00, 0x22 }, false);
            }
        }

        public static class Crc16
        {
            #region Private fields and properties

            private const ushort _polynomial = 0xA001;
            private static readonly ushort[] _table = new ushort[256];

            #endregion

            #region Constructor

            static Crc16()
            {
                for (ushort i = 0; i < _table.Length; ++i)
                {
                    ushort value = 0;
                    ushort temp = i;
                    for (byte j = 0; j < 8; ++j)
                    {
                        if (((value ^ temp) & 0x0001) != 0)
                        {
                            value = (ushort)((value >> 1) ^ _polynomial);
                        }
                        else
                        {
                            value >>= 1;
                        }
                        temp >>= 1;
                    }
                    _table[i] = value;
                }
            }

            #endregion

            #region Public methods

            public static ushort ComputeChecksum(byte[] bytes)
            {
                ushort crc = 0;
                foreach (byte t in bytes)
                {
                    byte index = (byte)(crc ^ t);
                    crc = (ushort)((crc >> 8) ^ _table[index]);
                }
                return crc;
            }

            #endregion
        }
    }

    #region Enums

    public enum CmdType
    {
        Unknown,
        GetMassa,
        GetName,
        GetScalePar,
        GetSys,
        GetTare,
        GetWeight,
        SetDatetime,
        SetName,
        SetRegnum,
        SetTare,
        SetZero,
        ResponseParse,
    }

    #endregion
}
