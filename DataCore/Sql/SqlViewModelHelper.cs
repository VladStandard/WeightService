// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Protocols;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static DataCore.ShareEnums;

namespace DataCore.Sql
{
    public class SqlViewModelHelper : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static SqlViewModelHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static SqlViewModelHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        private PublishType _publishType = PublishType.Default;
        public PublishType PublishType
        {
            get => _publishType;
            private set
            {
                _publishType = value;
                OnPropertyChanged();
            }
        }
        private string _publishDescription = "";
        public string PublishDescription
        {
            get => _publishDescription;
            private set
            {
                _publishDescription = value;
                OnPropertyChanged();
            }
        }
        private string _sqlInstance = "";
        public string SqlInstance
        {
            get => _sqlInstance;
            private set
            {
                _sqlInstance = value;
                OnPropertyChanged();
            }
        }
        private List<TaskDirect> _tasks = new();
        public List<TaskDirect> Tasks
        {
            get => _tasks;
            private set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }
        private List<TaskTypeDirect> _taskTypes = new();
        public List<TaskTypeDirect> TaskTypes
        {
            get => _taskTypes;
            private set
            {
                _taskTypes = value;
                OnPropertyChanged();
            }
        }
        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;
        private HostEntity _host;
        public HostEntity Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyChanged();
            }
        }
        private ScaleEntity _scale;
        public ScaleEntity Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                OnPropertyChanged();
            }
        }
        private List<string>? _scales;
        public List<string>? Scales
        {
            get => _scales;
            set
            {
                _scales = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor and destructor

        public SqlViewModelHelper()
        {
            SetPublish();

            _host = new();
            _scale = new();
            Setup(0);
        }

        public void Setup(long scaleId)
        {
            TaskTypes = new List<TaskTypeDirect>();
            Tasks = new List<TaskDirect>();

            if (scaleId <= 0)
            {
                if (string.IsNullOrEmpty(Host.HostName))
                {
                    string hostName = NetUtils.GetLocalHostName(false);
                    Host = SqlUtils.GetHostEntity(hostName);
                }
                Scale = SqlUtils.GetScaleFromHost(Host.IdentityId);
            }
            else
            {
                Scale = SqlUtils.GetScale(scaleId);
            }

            List<ScaleEntity> scales = SqlUtils.DataAccess.Crud.GetEntities<ScaleEntity>(
                new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                new(DbField.Description)).ToList();
            Scales = new();
            scales.ForEach(scale => Scales.Add(scale.Description));
        }

        private void SetPublish()
        {
            PublishType = PublishType.Default;
            PublishDescription = "Неизвестный сервер";
            SqlInstance = GetInstance();
            if (SqlInstance.Equals("INS1"))
            {
                PublishType = PublishType.Debug;
                PublishDescription = "Тестовый сервер";
            }
            else if (SqlInstance.Equals("SQL2019"))
            {
                PublishType = PublishType.Dev;
                PublishDescription = "Сервер разработки";
            }
            else if (SqlInstance.Equals("LUTON"))
            {
                PublishType = PublishType.Release;
                PublishDescription = "Продуктовый сервер";
            }
        }

        public void SetupTasks(long? scaleId)
        {
            if (scaleId == null)
                return;

            TaskTypes = SqlUtils.GetTasksTypes();

            Tasks = new List<TaskDirect>();
            foreach (TaskTypeDirect taskType in TaskTypes)
            {
                TaskDirect? task = SqlUtils.GetTask(taskType.Uid, (long)scaleId);
                if (task == null)
                {
                    SqlUtils.SaveNullTask(taskType, (long)scaleId, true);
                    task = SqlUtils.GetTask(taskType.Uid, (long)scaleId);
                }
                if (task != null)
                    Tasks.Add(task);
            }
        }

        public bool IsTaskEnabled(ProjectsEnums.TaskType taskType)
        {
            if (Tasks == null)
                return false;
            // Table [TASKS] don't has records.
            if (Tasks.Count == 0)
                return true;

            foreach (TaskDirect task in Tasks)
            {
                if (string.Equals(task.TaskType.Name, taskType.ToString(), StringComparison.InvariantCultureIgnoreCase))
                {
                    return task.Enabled;
                }
            }
            return false;
        }

        #endregion

        #region Public and private methods

        private string GetInstance()
        {
            string result = string.Empty;
            SqlConnect.ExecuteReader(SqlQueries.DbSystem.Properties.GetInstance, (reader) =>
            {
                if (reader.Read())
                {
                    result = SqlConnect.GetValueAsString(reader, "InstanceName");
                }
            });
            return result;
        }

        #endregion
    }
}
