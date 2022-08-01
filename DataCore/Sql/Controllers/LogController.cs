// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Settings;
using DataCore.Sql.TableScaleModels;
using DataCore.Utils;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using static DataCore.ShareEnums;

namespace DataCore.Sql.Controllers
{
    public class LogController
    {
        #region Public and private fields and properties

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        public AppEntity? App { get; set; }
        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public HostEntity? Host { get; set; }

        #endregion

        #region Constructor and destructor

        public LogController()
        {
            //
        }

        #endregion

        #region Public and private methods

        public void Setup(string? hostName, string? appName)
        {
            HostEntity? host = DataAccess.CrudHost.GetEntity(hostName);

            if (host != null && !host.EqualsDefault())
                Host = host;

            AppEntity? app = DataAccess.CrudApp.GetOrCreateNew(appName);
            if (app != null && !app.EqualsDefault())
                App = app;
        }

        [Obsolete(@"Use LogError")]
        public void Error(Exception ex, string filePath, int lineNumber, string memberName)
        {
            long idLast = DataAccess.Crud.GetEntity<ErrorEntity>(null,
                new(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId;
            ErrorEntity error = new()
            {
                IdentityId = idLast + 1,
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                FilePath = filePath,
                LineNumber = lineNumber,
                MemberName = memberName,
                Exception = ex.Message,
                InnerException = ex.InnerException == null ? string.Empty : ex.InnerException.Message,
            };
            DataAccess.Crud.ExecuteTransaction((session) => { session.Save(error); }, filePath, lineNumber, memberName, true);
        }

        public void LogToFile(string localFileLog, string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            StreamWriter streamWriter;
            streamWriter = File.Exists(localFileLog) ? File.AppendText(localFileLog) : File.CreateText(localFileLog);
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
            streamWriter.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {nameof(message)}: {message}");
            streamWriter.Close();
            streamWriter.Dispose();
        }

        public void LogError(Exception ex, string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (ex != null)
                Log(ex.Message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
            if (ex?.InnerException != null)
                Log(ex.InnerException.Message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
        }

        public void LogError(string message, string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Log(message, LogType.Error, hostName, appName, filePath, lineNumber, memberName);
        }

        public void LogStop(string message, string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Stop, hostName, appName, filePath, lineNumber, memberName);

        public void LogInformation(string message, string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Information, hostName, appName, filePath, lineNumber, memberName);

        public void LogWarning(string message, string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Warning, hostName, appName, filePath, lineNumber, memberName);

        public void Log(string message, LogType logType,
            string? hostName = null, string? appName = null,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            StringUtils.SetStringValueTrim(ref filePath, 32, true);
            StringUtils.SetStringValueTrim(ref memberName, 32);
            byte logNumber = (byte)logType;
            StringUtils.SetStringValueTrim(ref message, 1024);
            LogTypeEntity? logTypeItem = DataAccess.Crud.GetEntity<LogTypeEntity>(
                new(new() { new(DbField.Number, DbComparer.Equal, logNumber) }));

            HostEntity? host = Host;
            AppEntity? app = App;

            if (!string.IsNullOrEmpty(hostName))
                host = DataAccess.CrudHost.GetEntity(hostName);
            if (!string.IsNullOrEmpty(appName))
                app = DataAccess.CrudApp.GetOrCreateNew(appName);

            LogEntity log = new()
            {
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                IsMarked = false,
                Host = host,
                App = app,
                LogType = logTypeItem,
                Version = AppVersion.Version,
                File = filePath,
                Line = lineNumber,
                Member = memberName,
                Message = message,
            };
            DataAccess.Crud.SaveEntity(log);
        }

        public void LogQuestion(string message, string filePath, string memberName, int lineNumber,
            string? hostName = null, string? appName = null)
        {
            Log(message, LogType.Question, hostName, appName, filePath, lineNumber, memberName);
        }

        public Guid SaveApp(string name)
        {
            StringUtils.SetStringValueTrim(ref name, 32);
            AppEntity app = new() { Name = name };
            DataAccess.Crud.SaveEntity(app);
            return app.IdentityUid;
        }

        public long? GetHostId(string name)
        {
            StringUtils.SetStringValueTrim(ref name, 150);
            HostEntity? host = DataAccess.Crud.GetEntity<HostEntity>(
                new(new() { new(DbField.Name, DbComparer.Equal, name) }));
            return host?.IdentityId;
        }

        #endregion
    }
}
