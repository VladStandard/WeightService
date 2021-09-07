// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataShareCore.Utils;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using static DataProjectsCore.Utils.LogEnums;

namespace DataProjectsCore.Utils
{
    public class LogEnums
    {
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
    }

    public class LogUtils
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LogUtils _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LogUtils Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public LogUtils()
        {
            HostDirect host = HostsUtils.TokenRead();
            string app = string.Empty;
            string version = string.Empty;
            string appVersion = AppVersionUtils.GetMainFormText(Assembly.GetExecutingAssembly());
            if (appVersion.Split(' ').Length > 1)
            {
                app = appVersion.Split(' ')[0];
                version = appVersion.Split(' ')[1];
            }
            Log = new LogDirect(host.Name, host.IdRRef, app, version);
        }

        #endregion

        #region Public and private fields and properties

        private LogDirect Log { get; }
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        private void Message(string message, LogType logType, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            DateTime dt = DateTime.Now;
            string? dtStamp = $"[{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}:{dt.Second:D2}]";
            string? msg = $"{dtStamp} {message}";
            bool isDebug = false;
            switch (logType)
            {
                //case LogType.Debug:
                //    _log4net?.Debug(msg);
                //    isDebug = true;
                //    break;
                case LogType.Error:
                    _log4net?.Error(msg);
                    isDebug = true;
                    break;
                case LogType.Stop:
                    _log4net?.Fatal(msg);
                    isDebug = true;
                    break;
                case LogType.Information:
                    _log4net?.Info(msg);
                    break;
                case LogType.Warning:
                    _log4net?.Warn(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
            if (isDebug)
            {
                string? msgDebug = $"{dtStamp} File: {filePath}. Method: {memberName}. Line: {lineNumber}.";
                _log4net?.Debug(msgDebug);
            }
        }

        //public void Debug(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        //{
        //    Message(message, LogType.Debug, filePath, memberName, lineNumber);
        //}

        public void Error(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", 
            [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, LogType.Error, filePath, memberName, lineNumber);
            Log.SaveError(message, filePath, memberName, lineNumber);
        }

        [Obsolete(@"Deprecated method. Use Stop.")]
        public void Fatal(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, LogType.Stop, filePath, memberName, lineNumber);
            Log.SaveStop(message, filePath, memberName, lineNumber);
        }

        public void Stop(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, LogType.Stop, filePath, memberName, lineNumber);
            Log.SaveStop(message, filePath, memberName, lineNumber);
        }

        public void Information(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, LogType.Information, filePath, memberName, lineNumber);
            Log.SaveInformation(message, filePath, memberName, lineNumber);
        }

        public void Warning(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, LogType.Warning, filePath, memberName, lineNumber);
            Log.SaveWarning(message, filePath, memberName, lineNumber);
        }

    }
}
