// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Settings;
using DataCore.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static DataCore.ShareEnums;

namespace DataCore.DAL.Controllers
{
    public class LogController
    {
        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        public AppEntity? App { get; set; }
        public HostEntity? Host { get; set; }
        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;

        #endregion

        #region Constructor and destructor

        public LogController()
        {
            //
        }

        #endregion

        #region Public and private methods

        [Obsolete(@"Use LogError")]
        public void Error(Exception ex, string filePath, int lineNumber, string memberName)
        {
            long idLast = DataAccess.Crud.GetEntity<ErrorEntity>(null, new FieldOrderEntity(DbField.IdentityId, DbOrderDirection.Desc)).IdentityId;
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

        public void LogError(Exception ex,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (ex != null)
                Log(ex.Message, LogType.Error, filePath, memberName, lineNumber);
            if (ex?.InnerException != null)
                Log(ex.InnerException.Message, LogType.Error, filePath, memberName, lineNumber);
        }

        public void LogError(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            Log(message, LogType.Error, filePath, memberName, lineNumber);
        }

        public void LogStop(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Stop, filePath, memberName, lineNumber);

        public void LogInformation(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Information, filePath, memberName, lineNumber);

        public void LogWarning(string message,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
            Log(message, LogType.Warning, filePath, memberName, lineNumber);

        private void Log(string message, LogType logType, string filePath, string memberName, int lineNumber)
        {
            StringUtils.SetStringValueTrim(ref filePath, 32, true);
            StringUtils.SetStringValueTrim(ref memberName, 32);
            byte logNumber = (byte)logType;
            StringUtils.SetStringValueTrim(ref message, 1024);
            LogTypeEntity? logTypeItem = DataAccess.Crud.GetEntity<LogTypeEntity>(
                new FieldListEntity(new Dictionary<string, object?> { { DbField.Number.ToString(), logNumber } }),
                null);
            LogEntity log = new() {
                CreateDt = DateTime.Now,
                ChangeDt = DateTime.Now,
                IsMarked = false,
                Host = Host,
                App = App,
                LogType = logTypeItem,
                Version = AppVersion.Version,
                File = filePath,
                Line = lineNumber,
                Member = memberName,
                Message = message,
            };
            DataAccess.Crud.SaveEntity(log);
        }

        public void SaveQuestion(string message, string filePath, string memberName, int lineNumber)
        {
            Log(message, LogType.Question, filePath, memberName, lineNumber);
        }

        public Guid SaveApp(string name)
        {
            StringUtils.SetStringValueTrim(ref name, 32);
            AppEntity app = new() { Name = name};
            DataAccess.Crud.SaveEntity(app);
            return app.IdentityUid;
        }

        public long? GetHostId(string name)
        {
            StringUtils.SetStringValueTrim(ref name, 150);
            HostEntity? host = DataAccess.Crud.GetEntity<HostEntity>(
                new FieldListEntity(new Dictionary<string, object?> { { DbField.Name.ToString(), name } }),
                null);
            return host?.IdentityId;
        }

        #endregion
    }
}
