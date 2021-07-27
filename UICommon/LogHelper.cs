// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using log4net;

namespace UICommon
{
    internal enum EnumLogType
    {
        Debug,
        Error,
        Fatal,
        Info,
        Warn,
    }

    public class LogHelper
    {
        #region Public and private fields and properties

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        #endregion

        #region Design pattern "Lazy Singleton"

        private static LogHelper _instance;
        public static LogHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public LogHelper()
        {
            SetupDefault();
        }

        public void SetupDefault()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Message(string message, EnumLogType logType, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            var dt = DateTime.Now;
            var dtStamp = $"[{dt.Year:D4}-{dt.Month:D2}-{dt.Day:D2} {dt.Hour:D2}:{dt.Minute:D2}:{dt.Second:D2}]";
            var msg = $"{dtStamp} {message}";
            var isDebug = false;
            switch (logType)
            {
                case EnumLogType.Debug:
                    _log?.Debug(msg);
                    isDebug = true;
                    break;
                case EnumLogType.Error:
                    _log?.Error(msg);
                    isDebug = true;
                    break;
                case EnumLogType.Fatal:
                    _log?.Fatal(msg);
                    isDebug = true;
                    break;
                case EnumLogType.Info:
                    _log?.Info(msg);
                    break;
                case EnumLogType.Warn:
                    _log?.Warn(msg);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
            }
            if (isDebug)
            {
                var msgDebug = $"{dtStamp} Файл {filePath}. Метод {memberName}. Строка {lineNumber}.";
                _log?.Debug(msgDebug);
            }
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Debug(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, EnumLogType.Debug, filePath, memberName, lineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Error(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, EnumLogType.Error, filePath, memberName, lineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Fatal(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, EnumLogType.Fatal, filePath, memberName, lineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Info(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, EnumLogType.Info, filePath, memberName, lineNumber);
        }

        [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
        public void Warn(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Message(message, EnumLogType.Warn, filePath, memberName, lineNumber);
        }

        #endregion
    }
}
