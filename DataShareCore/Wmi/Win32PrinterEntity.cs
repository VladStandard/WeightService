// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.Wmi
{
    public class Win32PrinterEntity
    {
        #region Public and private fields and properties

        public string Name { get; set; }
        public string DriverName { get; set; }
        public string PortName { get; set; }
        public string PrinterState { get; set; }
        public string Status { get; set; }
        public Win32PrinterStatusEnum PrinterStatus { get; set; }
        public string PrinterStatusDescription { get => GetPrinterStatusDescription(); }
        public string StatusDescription { get => GetStatusDescription(); }

        #endregion

        #region Constructor and destructor

        public Win32PrinterEntity(string name, string driverName, string portName, string status, string printerState, Win32PrinterStatusEnum printerStatus)
        {
            Name = name;
            DriverName = driverName;
            PortName = portName;
            Status = status;
            PrinterState = printerState;
            PrinterStatus = printerStatus;
        }

        #endregion

        #region Public and private methods

        public string GetPrinterStatusDescription()
        {
            return PrinterStatus switch
            {
                Win32PrinterStatusEnum.Idle => "Бездействие",
                Win32PrinterStatusEnum.Paused => "Пауза",
                Win32PrinterStatusEnum.Error => "Ошибка",
                Win32PrinterStatusEnum.PendingDeletion => "Ожидание печати", // Ожидание удаления
                Win32PrinterStatusEnum.PaperJam => "Застревание бумаги",
                Win32PrinterStatusEnum.PaperOut => "Выдача бумаги",
                Win32PrinterStatusEnum.ManualFeed => "Ручная подача",
                Win32PrinterStatusEnum.PaperProblem => "Проблема с бумагой",
                Win32PrinterStatusEnum.Offline => "Не в сети",
                Win32PrinterStatusEnum.IoActive => "Ввод-вывод активен",
                Win32PrinterStatusEnum.Busy => "Занято",
                Win32PrinterStatusEnum.Printing => "Печать",
                Win32PrinterStatusEnum.OutputBinFull => "Выходной лоток полон",
                Win32PrinterStatusEnum.NotAvailable => "Недоступно",
                Win32PrinterStatusEnum.Waiting => "Ожидание",
                Win32PrinterStatusEnum.Processing => "Обработка",
                Win32PrinterStatusEnum.Initialization => "Инициализация",
                Win32PrinterStatusEnum.WarmingUp => "Прогрев",
                Win32PrinterStatusEnum.TonerLow => "Мало тонера",
                Win32PrinterStatusEnum.NoToner => "Нет тонера",
                Win32PrinterStatusEnum.PagePunt => "Страница беспечатана",
                Win32PrinterStatusEnum.UserInterventionRequired => "Требуется вмешательство пользователя",
                Win32PrinterStatusEnum.OutOfMemory => "Недостаточно памяти",
                Win32PrinterStatusEnum.DoorOpen => "Открыта дверца",
                Win32PrinterStatusEnum.Server_Unknown => "Сервер неизвестен",
                Win32PrinterStatusEnum.PowerSave => "Энергосбережение",
                _ => string.Empty,
            };
        }

        public string GetStatusDescription()
        {
            return Status switch
            {
                "OK" => "ОК",
                "Error" => "Ошибка",
                "Degraded" => "Оффлайн", // Деградирован
                "Unknown" => "Неизвестно",
                "Pred Fail" => "Неудача",
                "Starting" => "Запуск",
                "Stopping" => "Остановка",
                "Service" => "Сервис",
                "Stressed" => "Стресс",
                "Не восстанавливается" => "111",
                "Нет контакта" => "111",
                "Lost Comm" => "Потерянная связь",
                _ => string.Empty,
            };
        }

        #endregion
    }
}
