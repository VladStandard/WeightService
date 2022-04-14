// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.TableDirectModels;
using DataCore.DAL.Utils;
using DataCore.Helpers;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DataCore.DAL.DataModels
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

            _logDirect = new LogDirect(host.Name ?? string.Empty, host.IdRRef, AppVersion.App, AppVersion.Version);
        }

        #endregion

        #region Public and private fields and properties

        private readonly LogDirect _logDirect;
        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;

        #endregion

        #region Public and private methods

        public void Error(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            _logDirect.SaveError(message, filePath, memberName, lineNumber);

        public void Stop(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            _logDirect.SaveStop(message, filePath, memberName, lineNumber);

        public void Information(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            _logDirect.SaveInformation(message, filePath, memberName, lineNumber);

        public void Warning(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            _logDirect.SaveWarning(message, filePath, memberName, lineNumber);

        #endregion
    }
}
