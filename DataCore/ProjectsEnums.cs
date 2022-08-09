// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCore
{
    public class ProjectsEnums
    {
        public enum TaskType
        {
            Default,
            MassaManager,
            MemoryManager,
            PrintManager,
            LabelManager,
        }

        public enum DefaultSetting
        {
            All,
            ComPortName,
            SendTimeout,
            ReceiveTimeout,
            ZebraTcpAddress,
            ZebraTcpPort,
            Description,
            Guid,
            ConnectionString
        }

        public enum SilentUI
        {
            True,
            False,
        }

        public enum Direction
        {
            Left,
            Right,
        }

        public enum Page
        {
            Default,
            MessageBox,
            PinCode,
            PluList,
            ScaleChange,
            SqlSettings,
        }

        public enum OrderStatus
        {
            New = 0,
            InProgress = 1,
            Paused = 2,
            Performed = 3,
            Canceled = 4
        }

        public enum TableSystem
        {
            Default,
            Accesses,
            Logs,
            LogTypes,
            Versions,
            Tasks,
            TasksTypes,
        }

        public static TableSystem GetTableSystem(string tableName)
        {
            if (Enum.TryParse(tableName, out TableSystem tableSystem))
                return tableSystem;
            return TableSystem.Default;
        }

        public enum TableScale
        {
            Default,
            BarCodes,
            BarCodeTypes,
            Contragents,
            Hosts,
            Labels,
            Nomenclatures,
            Orders,
            OrdersStatuses,
            OrdersTypes,
            Organizations,
            Plus,
			PlusV2,
            PluRefs,
            Printers,
            PrintersResources,
            PrintersTypes,
            ProductionFacilities,
            ProductSeries,
            Scales,
            Templates,
            TemplatesResources,
            WeithingFacts,
            Workshops,
        }

        public static TableScale GetTableScale(string tableName)
        {
            if (Enum.TryParse(tableName, out TableScale tableScale))
                return tableScale;
            return TableScale.Default;
        }

        public enum TableDwh
        {
            Default,
            InformationSystem,
            Nomenclature,
            NomenclatureMaster,
            NomenclatureNonNormalize,
        }

        public static TableDwh GetTableDwh(string tableName)
        {
            if (Enum.TryParse(tableName, out TableDwh tableDwh))
                return tableDwh;
            return TableDwh.Default;
        }

        public enum WpfActivePage
        {
            About,
            ChangeLog,
            PinCode,
            Home,
            Settings,
        }

        /// <summary>
        /// Сообщение мыши.
        /// </summary>
        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_MBUTTONDOWN = 0x0207
        }

        /// <summary>
        /// Параметры отображения окна.
        /// </summary>
        public enum CmdShow
        {
            SW_HIDE, // Скрывает окно и активизирует другое окно.
            SW_MAXIMIZE, // Развертывает определяемое окно.
            SW_MINIMIZE, // Свертывает определяемое окно и активизирует следующее окно верхнего уровня в Z-последовательности.
            SW_RESTORE, // Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает в его первоначальных размерах и позиции.

            // Прикладная программа должна установить этот флажок при восстановлении свернутого окна.
            SW_SHOW, //  Активизирует окно и отображает его текущие размеры и позицию.
            SW_SHOWDEFAULT, // Устанавливает состояние показа, основанное на флажке SW_, определенном в структуре STARTUPINFO, 

            // переданной в функцию CreateProcess программой, которая запустила прикладную программу.
            SW_SHOWMAXIMIZED, //  Активизирует окно и отображает его как развернутое окно.
            SW_SHOWMINIMIZED, // Активизирует окно и отображает его как свернутое окно.
            SW_SHOWMINNOACTIVE, // Отображает окно как свернутое окно. Активное окно остается активным.
            SW_SHOWNA, // Отображает окно в его текущем состоянии. Активное окно остается активным.
            SW_SHOWNOACTIVATE, // Отображает окно в его самом современном размере и позиции. Активное окно остается активным.
            SW_SHOWNORMAL, //  Активизирует и отображает окно. Если окно свернуто или развернуто, Windows восстанавливает его в первоначальном размере и позиции.
                           // Прикладная программа должна установить этот флажок при отображении окна впервые.
        }

        /// <summary>
        /// Действие принтера Зебры.
        /// </summary>
        public enum ZebraAction
        {
            /// <summary>
            /// Сбросить принтер.
            /// </summary>
            Reset,
            /// <summary>
            /// Калибровать принтер.
            /// </summary>
            Calibrate,
            /// <summary>
            /// Список шрифтов.
            /// </summary>
            FontsList,
            /// <summary>
            /// Загрузить шрифты.
            /// </summary>
            LoadFonts,
            /// <summary>
            /// Очистить шрифты.
            /// </summary>
            ClearFonts,
            /// <summary>
            /// ~WC.
            /// </summary>
            WC,
            /// <summary>
            /// Загрузить картинки.
            /// </summary>
            LoadImages,
        }

        /// <summary>
        /// Уровень подробности строки подключения.
        /// </summary>
        public enum ConStringLevel
        {
            Basic,
            Low,
            Middle,
            Full,
        }
    }
}
