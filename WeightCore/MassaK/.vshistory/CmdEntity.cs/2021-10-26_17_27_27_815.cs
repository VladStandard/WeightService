// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;

namespace WeightCore.MassaK
{
    public abstract class CmdEntity
    {
        public abstract byte[] Bytes();
    }

    public class CmdSetTare : CmdEntity
    {
        //Установить тару
        //Структура сообщения:
        //byte Header[0] 0xF8 заголовочная последовательность
        //byte Header[1] 0x55 заголовочная последовательность
        //byte Header[2] 0xCE заголовочная последовательность
        //word Len 0x0005 длина тела сообщения
        //byte Command 0xA3 CMD_TCP_SET_TARE
        //int Tare 4 байта Масса тары в делениях
        //word CRC 2 байта CRC
        public static readonly byte[] CMD_TCP_SET_TARE = { 0xF8, 0x55, 0xCE, 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public int WeightTare { get; set; }
        public int ScaleFactor { get; set; } = 1000;

        public override byte[] Bytes()
        {
            byte[] data = new byte[CMD_TCP_SET_TARE.Length];
            for (int i = 0; i < CMD_TCP_SET_TARE.Length; i++)
            {
                data[i] = CMD_TCP_SET_TARE[i];
            }
            data[6] = (byte)(WeightTare & 0xFF);
            data[7] = (byte)((byte)(WeightTare >> 0x08) & 0xFF);
            data[8] = (byte)((byte)(WeightTare >> 0x16) & 0xFF);
            data[9] = (byte)((byte)(WeightTare >> 0x32) & 0xFF);

            data[10] = ScaleFactor switch
            {
                10000 => 0x00,
                1000 => 0x01,
                100 => 0x02,
                10 => 0x03,
                1 => 0x04,
                _ => 0x01,
            };
            byte[] selected = data.Skip(5).Take(9).ToArray();
            _ = selected.Reverse();

            // Посчитать CRC-код.
            //short crc = ComputeCRC16CCITT(0, selected, 1);
            ushort crc = Crc16.ComputeChecksum(selected);
            //var crc2 = Utils.Crc16.ComputeCRC16CCITT(0, selected, 1);

            data[data.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            data[data.Length - 1] = (byte)(crc & 0xFF);

            return data;
        }
    }

    public class CmdSetZero : CmdEntity
    {
        //В ряде весовых устройств команда не поддерживается (весовое устройство отвечает командой «CMD_NACK»).
        //Структура сообщения:
        //byte Header[0] 0xF8 заголовочная последовательность
        //byte Header[1] 0x55 заголовочная последовательность
        //byte Header[2] 0xCE заголовочная последовательность
        //int16 Len 0x0001 длина тела сообщения
        //byte Command 0x72 Код команды CMD_SET_ZERO
        //int16 CRC 2 байта CRC (см. Приложение 7.1)
        private static readonly byte[] CMD_TCP_SET_ZERO = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x72, 0x00, 0x00 };

        public override byte[] Bytes()
        {
            byte[] data = new byte[CMD_TCP_SET_ZERO.Length];
            for (int i = 0; i < CMD_TCP_SET_ZERO.Length; i++)
            {
                data[i] = CMD_TCP_SET_ZERO[i];
            }

            byte[] selected = data.Skip(5).Take(1).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);

            data[data.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            data[data.Length - 1] = (byte)(crc & 0xFF);

            return data;
        }
    }

    public class CmdGetMassa : CmdEntity
    {
        //CMD_GET_MASSA – запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары
        private static readonly byte[] CMD_GET_MASSA = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x23, 0x00, 0x00 };

        public override byte[] Bytes()
        {
            byte[] data = new byte[CMD_GET_MASSA.Length];
            for (int i = 0; i < CMD_GET_MASSA.Length; i++)
            {
                data[i] = CMD_GET_MASSA[i];
            }

            byte[] selected = data.Skip(5).Take(1).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);

            data[data.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            data[data.Length - 1] = (byte)(crc & 0xFF);

            return data;
        }
    }

    public class CmdGetScalePar : CmdEntity
    {
        //Запрос установленной массы тары и цены деления
        //Структура сообщения:
        //byte Header[0] 0xF8 заголовочная последовательность
        //byte Header[1] 0x55 заголовочная последовательность
        //byte Header[2] 0xCE заголовочная последовательность
        //word Len 0x0001 длина тела сообщения
        //byte Command 0x75 CMD_GET_SCALE_PAR
        //word CRC 2 байта CRC
        public static readonly byte[] CMD_GET_SCALE_PAR = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x75, 0x00, 0x00 };

        public override byte[] Bytes()
        {
            byte[] data = new byte[CMD_GET_SCALE_PAR.Length];
            for (int i = 0; i < CMD_GET_SCALE_PAR.Length; i++)
            {
                data[i] = CMD_GET_SCALE_PAR[i];
            }

            byte[] selected = data.Skip(5).Take(1).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);

            data[data.Length - 2] = (byte)(crc >> 0x08 & 0xFF);
            data[data.Length - 1] = (byte)(crc & 0xFF);

            return data;
        }
    }

    // CMD_ACK_SET_TARE (0x12) – команда установки тары выполнена успешно

    // CMD_NACK_TARE (0x15) – ошибка выполнения команды: невозможно установить тару

    //public struct CmdSetName
    //{
    //    //Запрос установленной массы тары и цены деления
    //    //Структура сообщения:
    //    //byte Header[0] 0xF8 заголовочная последовательность
    //    //byte Header[1] 0x55 заголовочная последовательность
    //    //byte Header[2] 0xCE заголовочная последовательность
    //    //word Len 0x0001 длина тела сообщения
    //    //byte Command 0x22 CMD_SET_NAME
    //    //word CRC 2 байта CRC
    //    public static readonly byte[] CMD_SET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x22 };

    //    public static byte[] Get(string name = "xx")
    //    {
    //        var data = new byte[CMD_SET_NAME.Length + name.Length + 2];
    //        int k = 0;
    //        for (var i = 0; i < CMD_SET_NAME.Length; i++)
    //        {
    //            data[i] = CMD_SET_NAME[i];
    //            k++;
    //        }

    //        for (var i = 0; (i < name.Length && i < 27); i++, k++)
    //        {
    //            data[k] = (byte)name.ToArray<char>()[i];
    //            k++;
    //        }

    //        data[k++] = 0x00;
    //        data[k++] = 0x00;

    //        data[4] = (byte)(1 + name.Length);
    //        data[5] = 0x00;


    //        var selected = data.Skip(6).Take(1 + name.Length).ToArray();
    //        selected.Reverse();
    //        var crc = Crc16.ComputeChecksum(selected);

    //        data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
    //        data[data.Length - 1] = (byte)(crc & 0xFF);

    //        return data;
    //    }

    //}

    //public struct CmdGetName
    //{
    //    //Запрос установленной массы тары и цены деления
    //    //Структура сообщения:
    //    //byte Header[0] 0xF8 заголовочная последовательность
    //    //byte Header[1] 0x55 заголовочная последовательность
    //    //byte Header[2] 0xCE заголовочная последовательность
    //    //word Len 0x0001 длина тела сообщения
    //    //byte Command 0x20 CMD_GET_NAME
    //    //word CRC 2 байта CRC
    //    public static readonly byte[] CMD_TCP_GET_TARE = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x20, 0x00, 0x00 };

    //    public static byte[] Get()
    //    {
    //        var data = new byte[CMD_TCP_GET_TARE.Length];
    //        for (var i = 0; i < CMD_TCP_GET_TARE.Length; i++)
    //        {
    //            data[i] = CMD_TCP_GET_TARE[i];
    //        }

    //        var selected = data.Skip(5).Take(1).ToArray();
    //        selected.Reverse();
    //        var crc = Crc16.ComputeChecksum(selected);

    //        data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
    //        data[data.Length - 1] = (byte)(crc & 0xFF);

    //        return data;
    //    }
    //}

    //public struct CmdResponseNameStruct
    //{
    //    public byte[] Data;
    //    public byte Header0;
    //    public byte Header1;
    //    public byte Header2;

    //    public Int16 Len;
    //    public byte Command;
    //    public Int32 ScalesID;
    //    public char[] Name;
    //    public Int16 CRC;

    //    public void Parse()
    //    {
    //        Header0 = Data[0];
    //        Header1 = Data[1];
    //        Header2 = Data[2];
    //        Len = BitConverter.ToInt16(Data.Skip(3).Take(2).ToArray(), 0);
    //        Command = Data[5];
    //        ScalesID = BitConverter.ToInt16(Data.Skip(6).Take(2).ToArray(), 0);
    //        //Name 
    //        //CRC = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);


    //    }

    //}

    //public struct CmdTcpSetRegnum
    //{
    //    //2. Сохранить номер регистрации
    //    //Запрос:
    //    //F8 55 CE 06 00 55 07 2C 01 00 00 44 1F
    //    public static readonly byte[] CMD_TCP_SET_RGNUM = { 0xF8, 0x55, 0xCE, 0x06, 0x00, 0x55, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

    //    public static byte[] Get(int Regnum)
    //    {
    //        var data = new byte[CMD_TCP_SET_RGNUM.Length];
    //        for (var i = 0; i < CMD_TCP_SET_RGNUM.Length; i++)
    //        {
    //            data[i] = CMD_TCP_SET_RGNUM[i];
    //        }

    //        data[7] = (byte)(Regnum & 0xFF);
    //        data[8] = (byte)((byte)(Regnum >> 0x08) & 0xFF);
    //        data[9] = (byte)((byte)(Regnum >> 0x16) & 0xFF);
    //        data[10] = (byte)((byte)(Regnum >> 0x32) & 0xFF);

    //        var selected = data.Skip(5).Take(6).ToArray();
    //        selected.Reverse();

    //        // Посчитать CRC-код.
    //        //short crc = ComputeCRC16CCITT(0, selected, 1);
    //        var crc = Crc16.ComputeChecksum(selected);
    //        //var crc2 = Utils.Crc16.ComputeCRC16CCITT(0, selected, 1);

    //        data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
    //        data[data.Length - 1] = (byte)(crc & 0xFF);

    //        return data;
    //    }
    //}

    //public struct CmdTcpSetDatetime
    //{
    //    //4. Установить дату/время
    //    //Запрос:
    //    //F8 55 CE 08 00 55 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>
    //    //где 13 0C 0B 00 00 00 - дата/время(00:00:00 11.12.19)
    //    //Ответ(всё ОК) :
    //    //F8 55 CE 06 00 56 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>
    //    public static readonly byte[] CMD_TCP_SET_DATETIME = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

    //    public static byte[] Get(DateTime dt)
    //    {
    //        var data = new byte[CMD_TCP_SET_DATETIME.Length];
    //        for (var i = 0; i < CMD_TCP_SET_DATETIME.Length; i++)
    //        {
    //            data[i] = CMD_TCP_SET_DATETIME[i];
    //        }

    //        data[7] = (byte)(dt.Year & 0xFF);
    //        data[8] = (byte)((byte)(dt.Month >> 0xFF) & 0xFF);
    //        data[9] = (byte)((byte)(dt.Day >> 0xFF) & 0xFF);
    //        data[10] = (byte)((byte)(dt.Hour >> 0xFF) & 0xFF);
    //        data[11] = (byte)((byte)(dt.Minute >> 0xFF) & 0xFF);
    //        data[12] = (byte)((byte)(dt.Second >> 0xFF) & 0xFF);

    //        return data;
    //    }
    //}

    //public struct CmdTcpGetSys
    //{
    //    //Прочитать служебную информацию
    //    //Структура сообщения:
    //    //byte Header[0] 0xF8 заголовочная последовательность
    //    //byte Header[1] 0x55 заголовочная последовательность
    //    //byte Header[2] 0xCE заголовочная последовательность
    //    //F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
    //    //
    //    public static readonly byte[] CMD_TCP_GET_SYS = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF };

    //    public static byte[] Get()
    //    {
    //        var data = new byte[CMD_TCP_GET_SYS.Length];
    //        for (var i = 0; i < CMD_TCP_GET_SYS.Length; i++)
    //        {
    //            data[i] = CMD_TCP_GET_SYS[i];
    //        }

    //        return data;
    //    }
    //}

    //public struct CmdResponseStruct
    //{
    //    public byte[] data;
    //    public byte Header0;
    //    public byte Header1;
    //    public byte Header2;

    //    public Int16 Len;
    //    public byte Command;
    //    public Int16 CRC;
    //    public bool Parse()
    //    {
    //        Header0 = data[0];
    //        Header1 = data[1];
    //        Header2 = data[2];
    //        Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
    //        Command = data[5];
    //        CRC = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

    //        var selected = data.Skip(5).Take(Len).ToArray();
    //        selected.Reverse();
    //        var crc = Crc16.ComputeChecksum(selected);
    //        return (Int16)CRC == (Int16)crc;

    //    }
    //}

    //public struct CmdTcpGetTare
    //{
    //    //Запрос установленной массы тары и цены деления
    //    //Структура сообщения:
    //    //byte Header[0] 0xF8 заголовочная последовательность
    //    //byte Header[1] 0x55 заголовочная последовательность
    //    //byte Header[2] 0xCE заголовочная последовательность
    //    //word Len 0x0001 длина тела сообщения
    //    //byte Command 0xA1 CMD_TCP_GET_TARE
    //    //word CRC 2 байта CRC
    //    public static readonly byte[] CMD_TCP_GET_TARE = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA1, 0x00, 0x00 };

    //    public static byte[] Get()
    //    {
    //        var data = new byte[CMD_TCP_GET_TARE.Length];
    //        for (var i = 0; i < CMD_TCP_GET_TARE.Length; i++)
    //        {
    //            data[i] = CMD_TCP_GET_TARE[i];
    //        }

    //        var selected = data.Skip(5).Take(1).ToArray();
    //        selected.Reverse();
    //        var crc = Crc16.ComputeChecksum(selected);

    //        data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
    //        data[data.Length - 1] = (byte)(crc & 0xFF);

    //        return data;
    //    }
    //}

    //public struct CmdTcpGetWeight
    //{
    //    //Запрос текущей массы, цены деления и признака стабильности показаний
    //    //Структура сообщения:
    //    //byte Header[0] 0xF8 заголовочная последовательность
    //    //byte Header[1] 0x55 заголовочная последовательность
    //    //byte Header[2] 0xCE заголовочная последовательность
    //    //word Len 0x0001 длина тела сообщения
    //    //byte Command 0xA0 CMD_TCP_GET_WEIGHT
    //    //word CRC 2 байта CRC
    //    private static readonly byte[] CMD_TCP_GET_WEIGHT = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA0, 0x00, 0x00 };

    //    public static byte[] Get()
    //    {
    //        var data = new byte[CMD_TCP_GET_WEIGHT.Length];
    //        for (var i = 0; i < CMD_TCP_GET_WEIGHT.Length; i++)
    //        {
    //            data[i] = CMD_TCP_GET_WEIGHT[i];
    //        }

    //        var selected = data.Skip(5).Take(1).ToArray();
    //        selected.Reverse();
    //        var crc = Crc16.ComputeChecksum(selected);

    //        data[data.Length - 2] = (byte)((crc >> 0x08) & 0xFF);
    //        data[data.Length - 1] = (byte)(crc & 0xFF);

    //        return data;
    //    }
    //}
}
