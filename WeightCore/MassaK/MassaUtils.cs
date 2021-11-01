// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// Protokol_100 (r 5) 2018 V3.pdf

using System.Collections.Generic;
using System.Linq;

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
                /// Запрос параметров весов.
                /// F8 55 CE 01 00 00 00 00
                /// </summary>
                public static readonly byte[] CMD_INIT_1 = GetFull(new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00 }, false);

                /// <summary>
                /// F8 55 CE 01 00 29 29 00
                /// </summary>
                public static readonly byte[] CMD_INIT_2 = GetFull(new byte[] { 0x01, 0x00, 0x29, 0x29, 0x00 }, false);

                /// <summary>
                /// F8 55 CE 01 00 20 20 00
                /// </summary>
                public static readonly byte[] CMD_INIT_3 = GetFull(new byte[] { 0x01, 0x00, 0x20, 0x20, 0x00 }, false);

                /// <summary>
                /// Запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары.
                /// F8 55 CE 01 00 23 00 00
                /// </summary>
                public static readonly byte[] CMD_GET_MASSA = GetFull(new byte[] { 0x01, 0x00, 0x23, 0x23, 0x00 }, false);

                /// <summary>
                /// Запрос параметров весового устройства.
                /// F8 55 CE 01 00 75 75 00
                /// </summary>
                public static readonly byte[] CMD_GET_SCALE_PAR = GetFull(new byte[] { 0x01, 0x00, 0x75, 0x75, 0x00 }, false);

                /// <summary>
                /// Запрос идентификатора весов.
                /// F8 55 CE 01 00 90 90 00
                /// </summary>
                public static readonly byte[] CMD_GET_ID = GetFull(new byte[] { 0x01, 0x00, 0x90, 0x90, 0x00 }, false);

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x20, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_NAME = GetFull(new byte[] { 0x01, 0x00, 0x20 });

                /// <summary>
                /// Запрос установленной массы тары и цены деления.
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_TARE = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA1, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_TARE = GetFull(new byte[] { 0x01, 0x00, 0xA1 });

                /// <summary>
                /// Прочитать служебную информацию.
                /// F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_SYS = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF };
                public static readonly byte[] CMD_GET_SYS = GetFull(new byte[] { 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF }, false);

                /// <summary>
                /// Запрос текущей массы, цены деления и признака стабильности показаний.
                /// </summary>
                /// public static readonly byte[] CMD_TCP_GET_WEIGHT = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA0, 0x00, 0x00 };
                public static readonly byte[] CMD_GET_WEIGHT = GetFull(new byte[] { 0x01, 0x00, 0xA0 });
            }

            public static class Set
            {
                /// <summary>
                /// Установить тару.
                /// 
                /// </summary>
                public static readonly byte[] CMD_SET_TARE = GetFull(new byte[] { 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00 });

                /// <summary>
                /// Установить >0<.
                /// F8 55 CE 01 00 72 72 00
                /// </summary>
                public static readonly byte[] CMD_SET_ZERO = GetFull(new byte[] { 0x01, 0x00, 0x72, 0x72, 0x00 }, false);

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
                /// </summary>
                /// public static readonly byte[] CMD_SET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x22 };
                public static readonly byte[] CMD_SET_NAME = GetFull(new byte[] { 0x01, 0x00, 0x22 }, false);
            }
        }

        public static string GetBytesAsHex(byte[] bytes) => string.Join(", ", bytes.Select(b => b.ToString("X2")));
    }

    #region Enums

    public enum CmdType
    {
        Unknown,
        Init1,
        Init2,
        Init3,
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
