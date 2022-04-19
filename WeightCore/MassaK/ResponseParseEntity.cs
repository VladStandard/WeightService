// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using static WeightCore.MassaK.MassaEnums;

namespace WeightCore.MassaK
{
    public class ResponseParseEntity
    {
        public MassaCmdType CmdType { get; }
        public byte[] Response { get; }
        public byte[] Header { get; private set; }
        public byte[] Len { get; private set; }
        public ushort LenAsUshort { get; private set; }
        public byte Command { get; }
        public byte[] Body { get; private set; }
        public byte[] Crc { get; private set; }
        public byte[] CrcCalc { get; private set; }
        public bool IsValidHeaders { get; private set; }
        public bool IsValidLength { get; private set; }
        public bool IsValidCommand { get; private set; }
        public bool IsValidCrc { get; private set; }
        public bool IsValidAll => IsValidHeaders && IsValidLength && IsValidCommand && IsValidCrc;
        public byte ErrorCode
        {
            get
            {
                if (Body == null || Body.Length < 6)
                    return 0x00;
                switch (CmdType)
                {
                    case MassaCmdType.GetScalePar:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.GetMassa:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.SetTare:
                        // -
                        break;
                    case MassaCmdType.SetZero:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.GetName:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.SetName:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.GetEthernet:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.SetEthernet:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.GetWiFiIp:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.SetWiFiIp:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.GetWiFiSsid:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.SetWiFiSsid:
                        if (Command == 0x28)
                            return Body[6];
                        break;
                    case MassaCmdType.Nack:
                        // -
                        break;
                }
                return 0x00;
            }
        }
        
        public string Message
        {
            get
            {
                switch (CmdType)
                {
                    case MassaCmdType.GetScalePar:
                        if (Command == 0x76) return "Запрос параметров весового устройства";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x17) return "Нет связи с модулем взвешивающим";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.GetMassa:
                        if (Command == 0x24) return "Запрос массы нетто, тары, стабильности";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x08) return "Нагрузка на весовом устройстве превышает НПВ";
                            else if (ErrorCode == 0x09) return "Весовое устройство не в режиме взвешивания";
                            else if (ErrorCode == 0x17) return "Нет связи с модулем взвешивающим";
                            else if (ErrorCode == 0x18) return "Установлена нагрузка на платформу при включении весового устройства";
                            else if (ErrorCode == 0x19) return "Весовое устройство неисправно";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.SetTare:
                        if (Command == 0x12) return "Команда установки тары выполнена успешно";
                        if (Command == 0x15)
                        {
                            return "Невозможно установить тару";
                        }
                        break;
                    case MassaCmdType.SetZero:
                        if (Command == 0x27) return "Команда установки >0< выполнена успешно";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x18) return "Установка >0< невозможна";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.GetName:
                        if (Command == 0x21) return "Запрос имени и ID весового устройства";
                        break;
                    case MassaCmdType.SetName:
                        if (Command == 0x27) return "Команда установки имени выполнена успешно";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x0A) return "Ошибка входных данных";
                            else if (ErrorCode == 0x0B) return "Ошибка сохранения данных";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.GetEthernet:
                        if (Command == 0x2E) return "Запрос параметров Ethernet";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x11) return "Интерфейс Ethernet не поддерживается";
                        }
                        break;
                    case MassaCmdType.SetEthernet:
                        if (Command == 0x27) return "Установка Ethernet выполнена успешно";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x0A) return "Ошибка входных данных";
                            else if (ErrorCode == 0x0B) return "Ошибка сохранения данных";
                            else if (ErrorCode == 0x11) return "Интерфейс Ethernet не поддерживается";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.GetWiFiIp:
                        if (Command == 0x34) return "Запрос IP-параметров подключения к сети Wi-Fi";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x10) return "Интерфейс WiFi не поддерживается";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.SetWiFiIp:
                        if (Command == 0x27) return "Передача IP-параметров подключения к сети Wi-Fi выполнена успешно";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x0A) return "Ошибка входных данных";
                            else if (ErrorCode == 0x0B) return "Ошибка сохранения данных";
                            else if (ErrorCode == 0x10) return "Интерфейс WiFi не поддерживается";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.GetWiFiSsid:
                        if (Command == 0x3B) return "Запрос параметров доступа к сети Wi-Fi";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x10) return "Интерфейс WiFi не поддерживается";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.SetWiFiSsid:
                        if (Command == 0x27) return "Передача параметров доступа к сети Wi-Fi выполнена успешно";
                        if (Command == 0x28)
                        {
                            if (ErrorCode == 0x0A) return "Ошибка входных данных";
                            else if (ErrorCode == 0x0B) return "Ошибка сохранения данных";
                            else if (ErrorCode == 0x10) return "Интерфейс WiFi не поддерживается";
                            return "Ошибка выполнения команды";
                        }
                        break;
                    case MassaCmdType.Nack:
                        if (Command == 0xF0) return "Принята неизвестная команда";
                        break;
                }
                return string.Empty;
            }
        }

        public ResponseMassaEntity Massa { get; set; }
        public ResponseScaleParEntity ScalePar { get; set; }
        private MassaCrcHelper MassaCrc { get; set; } = MassaCrcHelper.Instance;
        private MassaRequestHelper MassaRequest { get; set; } = MassaRequestHelper.Instance;

        public ResponseParseEntity(MassaCmdType cmdType, byte[] response)
        {
            CmdType = cmdType;
            Response = response;
            Header = new byte[3];
            IsValidHeaders = false;
            Len = new byte[2];
            IsValidLength = false;
            Command = new byte();
            IsValidCommand = false;
            IsValidCrc = false;

            if (Response?.Length > 0)
            {
                Header[0] = Response[0];
                Header[1] = Response[1];
                Header[2] = Response[2];
                IsValidHeaders = Header[0] == MassaRequest.Header[0] && Header[1] == MassaRequest.Header[1] && Header[2] == MassaRequest.Header[2];

                Len[0] = Response[3];
                Len[1] = Response[4];
                LenAsUshort = BitConverter.ToUInt16(Response.Skip(3).Take(2).ToArray(), 0);
                IsValidLength = LenAsUshort > 0;

                Command = Response[5];
                IsValidCommand = ErrorCode == 0x00;

                Body = Response.Skip(5).Take(LenAsUshort).ToArray();
                _ = Body.Reverse();

                Crc = Response.Skip(5 + LenAsUshort).Take(2).ToArray();
                CrcCalc = MassaCrc.CrcGet(Body);
                IsValidCrc = Crc[0] == CrcCalc[0] && Crc[1] == CrcCalc[1];

                switch (CmdType)
                {
                    case MassaCmdType.GetMassa:
                        Massa = new ResponseMassaEntity(Response);
                        break;
                    case MassaCmdType.GetScalePar:
                        ScalePar = new ResponseScaleParEntity(Response);
                        break;
                }
            }
        }
    }
}
