// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataShareCore
{
    public class ShareEnums
    {
        public enum Lang
        {
            English,
            Russian
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
        }

        public enum PublishType
        {
            Default,
            Dev,
            Debug,
            Release
        }

        public enum AccessRights
        {
            Admin,
            User,
            Guest
        }

        public enum DbTableAction
        {
            New,
            Edit,
            Copy,
            Mark,
            Delete,
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

        public enum DataLoad
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
            Uid,
            Id,
            Name,
            Value,
            Description,
            ScaleId,
            CategoryId,
            PrinterId,
            Title,
            CreateDate,
            ModifiedDate,
            Type,
            Plu,
            Marked,
            GoodsName,
            WeithingDate
        }

        public enum DbOrderDirection
        {
            Asc,
            Desc
        }
    }
}
