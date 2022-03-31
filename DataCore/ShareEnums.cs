// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore
{
    public class ShareEnums
    {
        public enum Lang
        {
            English,
            Russian
        }

        public enum AccessRights
        {
            None = 0,
            Read = 1,
            Write = 2,
            Admin = 3,
        }

        // https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.messageboxicon?view=net-5.0
        // SELECT * FROM [SCALES].[db_scales].[LOG_TYPES]
        // https://stackoverflow.com/questions/2031163/when-to-use-the-different-log-levels
        public enum LogType
        {
            None = 0,
            Error = 1,
            Stop = 2,
            Question = 3,
            Warning = 4,
            Information = 5,
            //Trace,
            //Debug,
            //Fatal,
        }

        public enum AppVerStringFormat
        {
            AsString,
            Use1,
            Use2,
            Use3,
            Use4,
        }

        public enum AppVerCountDigits
        {
            Use1,
            Use2,
            Use3,
            Use4,
        }

        public enum PublishType
        {
            Default,
            Dev,
            Debug,
            Release
        }

        public enum DbTableAction
        {
            Default,
            Cancel,
            Copy,
            Delete,
            Edit,
            Mark,
            New,
            Reload,
            Save,
        }

        public enum MemoryLimitAction
        {
            Exit,
            Restart
        }

        public enum RelevanceStatus
        {
            Unknown = 0,
            Actual = 1,
            NoActual = 2,
        }

        public enum NormilizationStatus
        {
            NotNormilized = 0,
            NormilizedFull = 1,
            NormilizedPart = 2,
            NotSubjectNormalization = 3,
        }

        public enum ActionLoad
        {
            None,
            Loading,
            Success,
            Error,
        }

        public enum DbType
        {
            Debug,
            Release,
        }

        public enum DbField
        {
            CategoryId,
            CodeInIs,
            CreateDt,
            CreateDate,
            Description,
            GoodsName,
            Id,
            IsMarked,
            ModifiedDate,
            Name,
            Plu,
            PrinterId,
            Scale_Id,
            ScaleId,
            Task_Uid,
            Title,
            Type,
            Uid,
            User,
            Value,
            WeithingDate,
        }

        public enum DbOrderDirection
        {
            Asc,
            Desc
        }

        public enum WindowResolution
        {
            Default,
            Res_800x600,
            Res_1024x768,
            Res_1366х768,
            Res_1920х1080,
        }

        public enum SettingsStorage
        {
            UseParams,
            UseConfig,
        }

        public enum Result
        {
            /// <summary>
            /// Ошибка.
            /// </summary>
            Error = -1,

            /// <summary>
            /// Выполнено успешно.
            /// </summary>
            Good = 0,
        }

        public enum WinVersion
        {
            /*
            +------------------------------------------------------------------------------+
            |                    |   PlatformID    |   Major version   |   Minor version   |
            +------------------------------------------------------------------------------+
            | Windows 95         |  Win32Windows   |         4         |          0        |
            | Windows 98         |  Win32Windows   |         4         |         10        |
            | Windows Me         |  Win32Windows   |         4         |         90        |
            | Windows NT 4.0     |  Win32NT        |         4         |          0        |
            | Windows 2000       |  Win32NT        |         5         |          0        |
            | Windows XP         |  Win32NT        |         5         |          1        |
            | Windows 2003       |  Win32NT        |         5         |          2        |
            | Windows Vista      |  Win32NT        |         6         |          0        |
            | Windows 2008       |  Win32NT        |         6         |          0        |
            | Windows 7          |  Win32NT        |         6         |          1        |
            | Windows 2008 R2    |  Win32NT        |         6         |          1        |
            | Windows 8          |  Win32NT        |         6         |          2        |
            | Windows 8.1        |  Win32NT        |         6         |          3        |
            +------------------------------------------------------------------------------+
            | Windows 10         |  Win32NT        |        10         |          0        |
            +------------------------------------------------------------------------------+
            */
            /// <summary>
            /// Не поддерживается.
            /// </summary>
            Unsupported = -1,

            /// <summary>
            /// Windows 7 x32.
            /// </summary>
            Win7x32 = 0,

            /// <summary>
            /// Windows 7 x64.
            /// </summary>
            Win7x64 = 1,

            /// <summary>
            /// Windows 10 x32.
            /// </summary>
            Win10x32 = 2,

            /// <summary>
            /// Windows 10 x64.
            /// </summary>
            Win10x64 = 3,
        }

        public enum WinProvider
        {
            /// <summary>
            /// Реестр.
            /// </summary>
            Registry = 0,

            /// <summary>
            /// Псевдонимы.
            /// </summary>
            Alias = 1,

            /// <summary>
            /// Окружение.
            /// </summary>
            Environment = 2,

            /// <summary>
            /// Файловая система.
            /// </summary>
            FileSystem = 3,

            /// <summary>
            /// Функции.
            /// </summary>
            Function = 4,

            /// <summary>
            /// Переменные.
            /// </summary>
            Variable = 5,

            /// <summary>
            /// Windows Management Instrumentation.
            /// </summary>
            Wmi = 6,
        }

        public enum StringTemplate
        {
            /// <summary>
            /// "text" or = "text".
            /// </summary>
            Equals = 0,

            /// <summary>
            /// "*text*" or like "%text%".
            /// </summary>
            Contains = 1,

            /// <summary>
            /// "text*" or like "text%".
            /// </summary>
            StartsWith = 2,

            /// <summary>
            /// "*text" or like "%text".
            /// </summary>
            EndsWith = 3,
        }

        public enum FormatType
        {
            Json,
            Xml,
            Html,
            Text,
            Raw
        }

        public enum ProgramState
        {
            Default,
            IsLoad,
            IsRun,
            IsExit
        }
    }
}
