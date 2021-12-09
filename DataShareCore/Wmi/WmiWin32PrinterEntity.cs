// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore.Wmi
{
    public class WmiWin32PrinterEntity
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

        public WmiWin32PrinterEntity(string name, string driverName, string portName, string status, string printerState, Win32PrinterStatusEnum printerStatus)
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
            if (LocalizationCore.Lang == ShareEnums.Lang.Russian)
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
                    Win32PrinterStatusEnum.ServerUnknown => "Сервер неизвестен",
                    Win32PrinterStatusEnum.PowerSave => "Энергосбережение",
                    _ => "Ошибка чтения статуса!",
                };
            }
            return PrinterStatus switch
            {
                Win32PrinterStatusEnum.Idle => "Idle",
                Win32PrinterStatusEnum.Paused => "Paused",
                Win32PrinterStatusEnum.Error => "Error",
                Win32PrinterStatusEnum.PendingDeletion => "Waiting for printing", // "Pending deletion"
                Win32PrinterStatusEnum.PaperJam => "Paper jam",
                Win32PrinterStatusEnum.PaperOut => "Paper out",
                Win32PrinterStatusEnum.ManualFeed => "Manual feed",
                Win32PrinterStatusEnum.PaperProblem => "Paper problem",
                Win32PrinterStatusEnum.Offline => "Offline",
                Win32PrinterStatusEnum.IoActive => "Io active",
                Win32PrinterStatusEnum.Busy => "Busy",
                Win32PrinterStatusEnum.Printing => "Printing",
                Win32PrinterStatusEnum.OutputBinFull => "Output bin full",
                Win32PrinterStatusEnum.NotAvailable => "Not available",
                Win32PrinterStatusEnum.Waiting => "Waiting",
                Win32PrinterStatusEnum.Processing => "Processing",
                Win32PrinterStatusEnum.Initialization => "Initialization",
                Win32PrinterStatusEnum.WarmingUp => "Warming up",
                Win32PrinterStatusEnum.TonerLow => "Toner low",
                Win32PrinterStatusEnum.NoToner => "No toner",
                Win32PrinterStatusEnum.PagePunt => "Page punt",
                Win32PrinterStatusEnum.UserInterventionRequired => "User intervention required",
                Win32PrinterStatusEnum.OutOfMemory => "Out of memory",
                Win32PrinterStatusEnum.DoorOpen => "Door open",
                Win32PrinterStatusEnum.ServerUnknown => "Server unknown",
                Win32PrinterStatusEnum.PowerSave => "Power save",
                _ => "Status reading error!",
            };
        }

        public string GetStatusDescription()
        {
            if (LocalizationCore.Lang == ShareEnums.Lang.Russian)
            {
                return Status switch
                {
                    "OK" => "ОК",
                    "Error" => "Ошибка",
                    "Degraded" => "Оффлайн",
                    "Unknown" => "Неизвестно",
                    "Pred Fail" => "Неудача",
                    "Starting" => "Запуск",
                    "Stopping" => "Остановка",
                    "Service" => "Сервис",
                    "Stressed" => "Стресс",
                    "Doesn't restore" => "Не восстанавливается",
                    "No contact" => "Нет контакта",
                    "Lost Comm" => "Потерянная связь",
                    _ => string.Empty,
                };
            }
            return Status switch
            {
                "OK" => "OK",
                "Error" => "Error",
                "Degraded" => "Degraded",
                "Unknown" => "Unknown",
                "Pred Fail" => "Pred Fail",
                "Starting" => "Starting",
                "Stopping" => "Stopping",
                "Service" => "Service",
                "Stressed" => "Stressed",
                "Doesn't restore" => "Doesn't restore",
                "No contact" => "No contact",
                "Lost Comm" => "Lost Comm",
                _ => string.Empty,
            };
        }

        #endregion
    }
}
