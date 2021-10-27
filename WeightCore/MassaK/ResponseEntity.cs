// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;

namespace WeightCore.MassaK
{
    public class ResponseFactory
    {
        public static ResponseEntity GetResponse(byte[] data)
        {
            if (!ResponseEntity.IsValidData(data))
                return null;
            // Cmd.
            return data[5] switch
            {
                // CMD_ACK_MASSA: 36 DEC
                0x24 => new ResponseGetMassa(data),
                // CMD_ERROR: 40 DEC
                0x28 => new ResponseError(data),
                // CMD_ACK_SCALE_PAR: 118 DEC
                0x76 => new ResponseScalePar(data),
                // CMD_ACK_SCALE_PAR: 18 DEC
                0x12 => new ResponseSetTare(data),
                // CMD_NACK_TARE: 21 DEC
                0x15 => new ResponseNackTare(data),
                // CMD_SET_ZERO: 39 DEC
                0x27 => new ResponseSetZero(data),
                // По-умолчанию.
                _ => null,
            };
        }
    }

    public abstract class ResponseEntity
    {
        public byte[] Data;
        public byte Header0;
        public byte Header1;
        public byte Header2;
        public short Len;
        public byte Command;
        public short Crc;
        public bool IsValid;

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

            short len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            if (len <= 0)
                return false;

            return true;
        }
    }

    public class ResponseError : ResponseEntity
    {
        public int ErrorCode;

        public ResponseError(byte[] data)
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
            ushort crc = Crc16.ComputeChecksum(selected);
            IsValid = Crc == (short)crc && Command == 0x28;
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

    public class ResponseSetTare : ResponseEntity
    {
        public ResponseSetTare(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);
            IsValid = Crc == (short)crc;

        }

        public override string GetMessage()
        {
            return "Код ответа CMD_ACK_SET_TARE";
        }


    }

    public class ResponseNackTare : ResponseEntity
    {
        public ResponseNackTare(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);
            IsValid = Crc == (short)crc;

        }

        public override string GetMessage()
        {
            return "0х15 – Установка >0< невозможна";
        }
    }

    public class ResponseSetZero : ResponseEntity
    {
        public ResponseSetZero(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Crc = BitConverter.ToInt16(data.Skip(6).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);
            IsValid = Crc == (short)crc;

        }

        public override string GetMessage()
        {
            return "Код ответа CMD_ACK_SET";
        }
    }

    public class ResponseGetMassa : ResponseEntity
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

        public ResponseGetMassa(byte[] data)
        {
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];
            Weight = BitConverter.ToInt32(data.Skip(6).Take(4).ToArray(), 0);
            Division = data[10];
            Stable = data[11];
            Net = data[12];
            Zero = data[13];
            Tare = BitConverter.ToInt32(data.Skip(14).Take(4).ToArray(), 0);
            Crc = BitConverter.ToInt16(data.Skip(18).Take(2).ToArray(), 0);

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            ushort crc = Crc16.ComputeChecksum(selected);
            IsValid = Crc == (short)crc && Command == 0x24;

        }

        public override string GetMessage()
        {
            return $"Текущая масса нетто со знаком {Weight}";
        }
    }

    public class ResponseScalePar : ResponseEntity
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

        public ResponseScalePar(byte[] data)
        {
            System.Text.ASCIIEncoding enc = new();
            Header0 = data[0];
            Header1 = data[1];
            Header2 = data[2];
            Len = BitConverter.ToInt16(data.Skip(3).Take(2).ToArray(), 0);
            Command = data[5];

            byte[] selected = data.Skip(5).Take(Len).ToArray();
            _ = selected.Reverse();
            _ = Crc16.ComputeChecksum(selected);

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
