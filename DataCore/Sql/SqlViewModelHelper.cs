// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using MvvmHelpers;
using NHibernate.Cfg;
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

        #region Public and private fields, properties, constructor

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

        private SqlConnectFactory SqlConnect { get; set; } = SqlConnectFactory.Instance;
        private HostEntity _host;
        public HostEntity Host
        {
            get => _host;
            private set
            {
                _host = value;
                OnPropertyChanged();
            }
        }
        private ScaleEntity _scale;
        public ScaleEntity Scale
        {
            get => _scale;
            private set
            {
                _scale = value;
                OnPropertyChanged();
            }
        }
        private string _area;
        public string Area
        {
            get
            {
                if (!string.IsNullOrEmpty(_area))
                    return _area;
                if (Scale.WorkShop != null)
                    return Scale.WorkShop.ProductionFacility.Name;
                return _area;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    if (Scale.WorkShop != null)
                        _area = Scale.WorkShop.ProductionFacility.Name;
                    return;
                }
                _area = value;
            }
        }
        private List<string> _scales;
        public List<string> Scales
        {
            get => _scales;
            private set
            {
                _scales = value;
                OnPropertyChanged();
            }
        }
        private List<string> _areas;
        public List<string> Areas
        {
            get => _areas;
            private set
            {
                _areas = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public SqlViewModelHelper()
        {
            SetPublish();

            _host = new();
            _scale = new();
            _scales = new();
            _area = string.Empty;
            _areas = new();
            Setup(-1, "");
        }

        #endregion

        #region Public and private methods

        public void Setup(long scaleId, string areaName)
        {
            TaskTypes = new();
            Tasks = new();
            SetScale(scaleId, areaName);
            SetScales();
            SetAreas();
        }

        private void SetScale(long scaleId, string areaName)
        {
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
            if (!string.IsNullOrEmpty(areaName))
                Area = areaName;
        }

        private void SetScales()
        {
            Scales = new();
            ScaleEntity[]? scales = SqlUtils.DataAccess.Crud.GetEntities<ScaleEntity>(
                new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                new(DbField.Description));
            if (scales != null)
            {
                List<ScaleEntity> scales2 = scales.ToList();
                scales2.ForEach(scale => Scales.Add(scale.Description));
            }
        }

        private void SetAreas()
        {
            Areas = new();
            ProductionFacilityEntity[]? areas = SqlUtils.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(
                new(new()
                {
                    new(DbField.IsMarked, DbComparer.Equal, false)
                }),
                new(DbField.Name));
            if (areas != null)
            {
                List<ProductionFacilityEntity> areas2 = areas.Where(x => x.IdentityId > 0).ToList();
                areas2.ForEach(area => Areas.Add(area.Name));
            }
        }

        private void SetPublish()
        {
            PublishType = PublishType.Default;
            PublishDescription = "Неизвестный сервер";
            SqlInstance = GetInstance();
            SetPublishFromInstance();
        }

        private void SetPublishFromInstance()
        {
            switch (SqlInstance)
            {
                case "INS1":
                    PublishType = PublishType.Debug;
                    PublishDescription = LocaleCore.Sql.SqlServerTest;
                    break;
                case "SQL2019":
                    PublishType = PublishType.Dev;
                    PublishDescription = LocaleCore.Sql.SqlServerDev;
                    break;
                case "LUTON":
                    PublishType = PublishType.Release;
                    PublishDescription = LocaleCore.Sql.SqlServerProd;
                    break;
            }
        }

        public void SetupTasks(long? scaleId)
        {
            if (scaleId == null)
                return;

            TaskTypes = SqlUtils.GetTasksTypes();

            Tasks = new();
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
