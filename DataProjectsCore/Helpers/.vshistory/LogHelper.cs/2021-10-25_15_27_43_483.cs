// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataShareCore;
using DataShareCore.Helpers;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DataProjectsCore.Helpers
{
    public class LogHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static LogHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static LogHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public LogHelper()
        {
            HostDirect host = HostsUtils.TokenRead();

            _logDb = new LogDirect(host.Name, host.IdRRef, _appVersion.App, _appVersion.Version);
        }

        #endregion

        #region Public and private fields and properties

        private readonly LogDirect _logDb;
        private readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;

        #endregion

        #region Public and private methods

        private void Log4netSave(string message, ShareEnums.LogType logType,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
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
                case ShareEnums.LogType.Error:
                    _log4net?.Error(msg);
                    isDebug = true;
                    break;
                case ShareEnums.LogType.Stop:
                    _log4net?.Fatal(msg);
                    isDebug = true;
                    break;
                case ShareEnums.LogType.Information:
                    _log4net?.Info(msg);
                    break;
                case ShareEnums.LogType.Warning:
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

        public void Error(string message, [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", 
            [CallerLineNumber] int lineNumber = 0)
        {
            Log4netSave(message, ShareEnums.LogType.Error, filePath, memberName, lineNumber);
            _logDb.SaveError(message, filePath, memberName, lineNumber);
        }

        [Obsolete(@"Deprecated method. Use Stop.")]
        public void Fatal(string message,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log4netSave(message, ShareEnums.LogType.Stop, filePath, memberName, lineNumber);
            _logDb.SaveStop(message, filePath, memberName, lineNumber);
        }

        public void Stop(string message,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log4netSave(message, ShareEnums.LogType.Stop, filePath, memberName, lineNumber);
            _logDb.SaveStop(message, filePath, memberName, lineNumber);
        }

        public void Information(string message,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log4netSave(message, ShareEnums.LogType.Information, filePath, memberName, lineNumber);
            _logDb.SaveInformation(message, filePath, memberName, lineNumber);
        }

        public void Warning(string message,
            [CallerFilePath] string filePath = "", [CallerMemberName] string memberName = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log4netSave(message, ShareEnums.LogType.Warning, filePath, memberName, lineNumber);
            _logDb.SaveWarning(message, filePath, memberName, lineNumber);
        }

        #endregion
    }
}
