// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WeightCore.MassaK
{
    public static class CmdQueries
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
        public static readonly byte[] CMD_TCP_SET_TARE = { 0xF8, 0x55, 0xCE, 0x05, 0x00, 0xA3, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        /// <summary>
        /// В ряде весовых устройств команда не поддерживается (весовое устройство отвечает командой «CMD_NACK»).
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// int16 Len 0x0001 длина тела сообщения
        /// byte Command 0x72 Код команды CMD_SET_ZERO
        /// int16 CRC 2 байта CRC (см. Приложение 7.1)
        /// </summary>
        public static readonly byte[] CMD_TCP_SET_ZERO = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x72, 0x00, 0x00 };

        /// <summary>
        /// Сохранить номер регистрации.
        /// F8 55 CE 06 00 55 07 2C 01 00 00 44 1F
        /// </summary>
        public static readonly byte[] CMD_TCP_SET_RGNUM = { 0xF8, 0x55, 0xCE, 0x06, 0x00, 0x55, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Запрос установленной массы тары и цены деления.
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// word Len 0x0001 длина тела сообщения
        /// byte Command 0x20 CMD_GET_NAME
        /// word CRC 2 байта CRC
        /// </summary>
        public static readonly byte[] CMD_TCP_GET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x20, 0x00, 0x00 };

        /// <summary>
        /// Запрос установленной массы тары и цены деления.
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// word Len 0x0001 длина тела сообщения
        /// byte Command 0xA1 CMD_TCP_GET_TARE
        /// word CRC 2 байта CRC
        /// </summary>
        public static readonly byte[] CMD_TCP_GET_TARE = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0xA1, 0x00, 0x00 };

        /// <summary>
        /// Установить дату/время.
        /// Запрос: F8 55 CE 08 00 55 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>, где 13 0C 0B 00 00 00 - дата/время (00:00:00 11.12.19)
        /// Ответ(всё ОК): F8 55 CE 06 00 56 01 13 0C 0B 00 00 00 <CRCLo> <CRCHi>
        /// </summary>
        public static readonly byte[] CMD_TCP_SET_DATETIME = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x55, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

        /// <summary>
        /// Прочитать служебную информацию.
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// F8 55 CE 08 00 92 01 00 00 00 00 00 00 B5 EF
        /// </summary>
        public static readonly byte[] CMD_TCP_GET_SYS = { 0xF8, 0x55, 0xCE, 0x08, 0x00, 0x92, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB5, 0xEF };

        /// <summary>
        /// Запрос массы нетто, массы тары, флагов стабильности, установки нуля и тары.
        /// </summary>
        public static readonly byte[] CMD_GET_MASSA = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x23, 0x00, 0x00 };

        /// <summary>
        /// Запрос установленной массы тары и цены деления.
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// word Len 0x0001 длина тела сообщения
        /// byte Command 0x75 CMD_GET_SCALE_PAR
        /// word CRC 2 байта CRC
        /// </summary>
        public static readonly byte[] CMD_GET_SCALE_PAR = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x75, 0x00, 0x00 };

        /// <summary>
        /// Запрос установленной массы тары и цены деления.
        /// byte Header[0] 0xF8 заголовочная последовательность
        /// byte Header[1] 0x55 заголовочная последовательность
        /// byte Header[2] 0xCE заголовочная последовательность
        /// word Len 0x0001 длина тела сообщения
        /// byte Command 0x22 CMD_SET_NAME
        /// word CRC 2 байта CRC
        /// </summary>
        public static readonly byte[] CMD_SET_NAME = { 0xF8, 0x55, 0xCE, 0x01, 0x00, 0x22 };

        // CMD_ACK_SET_TARE (0x12) – команда установки тары выполнена успешно

        // CMD_NACK_TARE (0x15) – ошибка выполнения команды: невозможно установить тару

    }
}
