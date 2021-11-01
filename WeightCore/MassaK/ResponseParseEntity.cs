// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;

namespace WeightCore.MassaK
{
    public abstract class ResponseParseEntity
    {
        public byte[] Data;
        public byte Header0;
        public byte Header1;
        public byte Header2;
        public short Len;
        public byte Command;
        public short Crc;
        //public bool IsValid;

        public abstract string GetMessage();

        public static bool IsValidData(byte[] data)
        {
            if (data == null || data.Length < 5)
                return false;

            byte header0 = data[0];
            byte header1 = data[1];
            byte header2 = data[2];
            if (!(header0 == 0xF8 && header1 == 0x55 && header2 == 0xCE))
                return false;
            // Len.
            return BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0) > 0;
        }
    }

    public class ResponseParseErrorEntity : ResponseParseEntity
    {
        public int ErrorCode;

        public ResponseParseErrorEntity(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[6];
            ErrorCode = data[7];
            Crc = BitConverter.ToInt16(data.Skip(7).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            //IsValid = Crc == (short)crc && Command == 0x28;
        }

        public override string GetMessage()
        {
            string msg = string.Empty;
            switch (ErrorCode)
            {
                case 0x07:  // 7 DEC
                    msg = "Команда не поддерживается";
                    break;
                case 0x08: // 8 DEC
                    msg = "Нагрузка на весовом устройстве превышает НПВ";
                    break;
                case 0x09: // 9 DEC
                    msg = "Весовое устройство не в режиме взвешивания";
                    break;
                case 0x0A: // 10 DEC
                    msg = "Ошибка входных данных";
                    break;
                case 0x0B: // 11 DEC
                    msg = "Ошибка сохранения данных";
                    break;
                case 0x10: // 16 DEC
                    msg = "Интерфейс WiFi не поддерживается";
                    break;
                case 0x11: // 17 DEC
                    msg = "Интерфейс Ethernet не поддерживается";
                    break;
                case 0x15: // 21 DEC
                    msg = "Установка >0< невозможна";
                    break;
                case 0x17: // 23 DEC
                    msg = "Нет связи с модулем взвешивающим";
                    break;
                case 0x18: // 24 DEC
                    msg = "Установлена нагрузка на платформу при включении весового устройства";
                    break;
                case 0x19: // 25 DEC
                    msg = "Весовое устройство неисправно";
                    break;
                case 0x28: // 40 DEC
                    msg = "Ошибка выполнения команды";
                    break;
                case 0xF0: // 240 DEC
                    msg = "Неизвестная ошибка";
                    break;
            };
            return msg;
        }
    }

    public class ResponseParseSetTareEntity : ResponseParseEntity
    {
        public ResponseParseSetTareEntity(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            //IsValid = Crc == (short)crc;
        }

        public override string GetMessage()
        {
            return "Код ответа CMD_ACK_SET_TARE";
        }
    }

    public class ResponseParseNackTareEntity : ResponseParseEntity
    {
        public ResponseParseNackTareEntity(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            //IsValid = Crc == (short)crc;
        }

        public override string GetMessage()
        {
            return "0х15 – Установка >0< невозможна";
        }
    }

    public class ResponseParseSetZeroEntity : ResponseParseEntity
    {
        public ResponseParseSetZeroEntity(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            //IsValid = Crc == (short)crc;
        }

        public override string GetMessage()
        {
            return "Код ответа CMD_ACK_SET";
        }
    }

    public class ResponseParseGetMassaEntity : ResponseParseEntity
    {
        public int Weight;
        public int ScaleFactor;
        public byte _division;
        public byte Division
        {
            get => _division;
            set
            {
                _division = value;
                ScaleFactor = value switch
                {
                    0x00 => 10000,
                    0x01 => 1000,
                    0x02 => 100,
                    0x03 => 10,
                    0x04 => 1,
                    _ => 1000,
                };
            }
        }
        public byte Stable;
        public byte Net;
        public byte Zero;
        public int Tare;

        public ResponseParseGetMassaEntity(byte[] response)
        {
            Header0 = response[0];
            Header1 = response[1];
            Header2 = response[2];
            Len = BitConverter.ToInt16(response.Skip(3).Take(2).ToArray(), 0);
            Command = response[5];
            Weight = BitConverter.ToInt32(response.Skip(6).Take(4).ToArray(), 0);
            Division = response[10];
            Stable = response[11];
            Net = response[12];
            Zero = response[13];
            Tare = BitConverter.ToInt32(response.Skip(14).Take(4).ToArray(), 0);
            Crc = BitConverter.ToInt16(response.Skip(18).Take(2).ToArray(), 0);

            byte[] selected = response.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //ushort crc = MassaUtils.Crc16.GetChecksum(selected);
            //IsValid = Crc == (short)crc && Command == 0x24;
        }

        public override string GetMessage()
        {
            return $"Текущая масса нетто со знаком {Weight}";
        }
    }

    public class ResponseParseScaleParEntity : ResponseParseEntity
    {
        //Максимальная нагрузка, Max
        public string P_Max;
        //Минимальная нагрузка, Min
        public string P_Min;
        //Поверочный интервал весов
        public string P_e;
        //Максимальная масса тары, T
        public string P_T;
        //Параметр фиксации веса
        public string Fix;
        //Код юстировки
        public string Calcode;
        //Версия ПО датчика взвешивания,
        public string PO_Ver;
        //Контрольная сумма ПО датчика взвешивания
        public string PO_Summ;

        public ResponseParseScaleParEntity(byte[] data)
        {
            System.Text.ASCIIEncoding enc = new();
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            //_ = MassaUtils.Crc16.GetChecksum(selected);
            _ = NullFX.CRC.Crc16.ComputeChecksum(NullFX.CRC.Crc16Algorithm.Ccitt, selected);

            // сюда надо вставить логику
            int i = 6;
            using MemoryStream memStream = new();
            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }

            P_Max = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);

            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            P_Min = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            P_e = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            P_T = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            Fix = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            Calcode = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            PO_Ver = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            while (data[i] != 0x0D)
            {
                memStream.WriteByte(data[i++]);
            }
            PO_Summ = enc.GetString(memStream.ToArray(), 0, memStream.ToArray().Length);
            i++; // пропустим 0x0D
            i++; // пропустим 0x0A
            memStream.SetLength(0);

            //IsValid = (Int16)CRC == (Int16)crc;
        }

        public override string GetMessage()
        {
            return $"Код ответа CMD_ACK_SCALE_PAR: {Command};\n{P_Max};\n{P_Min};\n{P_e};\n{P_T};\n{Fix};\n{Calcode};\n{PO_Ver};\n{PO_Summ}";
        }
    }
}
