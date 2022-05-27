// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDirectModels;
using DataCore.Localizations;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Threading;
using DataCore.Sql.Controllers;
using DataCore.Protocols;
using DataCore.Sql.TableScaleModels;
using DataCore.Sql.Models;
using static DataCore.ShareEnums;
using System.Linq;

namespace DataCore.Sql
{
    public class SqlViewModelEntity : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static SqlViewModelEntity _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static SqlViewModelEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        public DataAccessHelper DataAccess { get; private set; } = DataAccessHelper.Instance;
        private ShareEnums.PublishType _publishType = PublishType.Default;
        public ShareEnums.PublishType PublishType
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
        private string? _hostName;
        public string? HostName
        {
            get => _hostName;
            set
            {
                _hostName = value;
                OnPropertyChanged();
            }
        }
        private string? _scaleNameBackup;
        public string? ScaleNameBackup
        {
            get => _scaleNameBackup;
            private set
            {
                _scaleNameBackup = value;
                OnPropertyChanged();
            }
        }
        private string? _scaleName;
        public string? ScaleName
        {
            get => _scaleName;
            set
            {
                _scaleName = value;
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
        private int? _scaleNumber;
        public int? ScaleNumber
        {
            get => _scaleNumber;
            set
            {
                _scaleNumber = value;
                OnPropertyChanged();
            }
        }
        private string? _buttonRestoreDeviceCaption;
        public string? ButtonRestoreDeviceCaption
        {
            get => _buttonRestoreDeviceCaption;
            set
            {
                _buttonRestoreDeviceCaption = value;
                OnPropertyChanged();
            }
        }
        private string? _buttonOkCaption;
        public string? ButtonOkCaption
        {
            get => _buttonOkCaption;
            set
            {
                _buttonOkCaption = value;
                OnPropertyChanged();
            }
        }
        private string? _buttonCancelCaption;
        public string? ButtonCancelCaption
        {
            get => _buttonCancelCaption;
            set
            {
                _buttonCancelCaption = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor and destructor

        public SqlViewModelEntity()
        {
            SetPublish();

            Setup();
        }

        public void Setup(string scaleName = "")
        {
            TaskTypes = new List<TaskTypeDirect>();
            Tasks = new List<TaskDirect>();

            ScaleEntity scale;
            if (string.IsNullOrEmpty(scaleName))
            {
                HostName = NetUtils.GetLocalHostName(false);
                HostEntity host = HostsUtils.GetHostEntity(HostName);
                scale = HostsUtils.GetScaleEntity(host.IdentityId);
            }
            else
                scale = HostsUtils.GetScaleEntity(scaleName);
            ScaleNameBackup = ScaleName = scale.Description;
            ScaleNumber = scale.Number;

            List<ScaleEntity> scales = HostsUtils.DataAccess.Crud.GetEntities<ScaleEntity>(
                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc)).ToList();
            Scales = new();
            scales.ForEach(scale => Scales.Add(scale.Description));

            ButtonOkCaption = LocaleCore.Buttons.Ok;
            ButtonCancelCaption = LocaleCore.Buttons.Cancel;
            ButtonRestoreDeviceCaption = LocaleCore.Scales.RestoreDevice;
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

            TaskTypes = TasksTypeUtils.GetTasksTypes();

            Tasks = new List<TaskDirect>();
            foreach (TaskTypeDirect taskType in TaskTypes)
            {
                TaskDirect? task = TasksUtils.GetTask(taskType.Uid, (long)scaleId);
                if (task == null)
                {
                    TasksUtils.SaveNullTask(taskType, (long)scaleId, true);
                    task = TasksUtils.GetTask(taskType.Uid, (long)scaleId);
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
