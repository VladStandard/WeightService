// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore.DAL.TableModels;
using DataProjectsCore.DAL.Utils;
using DataShareCore;
using Microsoft.Data.SqlClient;
using MvvmHelpers;
using System.Collections.Generic;
using System.Threading;

namespace DataProjectsCore.DAL
{
    public class SqlViewModelEntity : BaseViewModel
    {
        #region Design pattern "Lazy Singleton"

        private static SqlViewModelEntity _instance;
        public static SqlViewModelEntity Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public SqlViewModelEntity() { SetupDefault(); }

        public void SetupDefault()
        {
            DataSource = string.Empty;
            DataBase = string.Empty;
            Host = string.Empty;
            PublishType = ShareEnums.PublishType.Default;
            PublishDescription = "Неизвестный сервер";

            SqlInstance = GetInstance();
            if (SqlInstance.Equals("INS1"))
            {
                PublishType = ShareEnums.PublishType.Debug;
                PublishDescription = "Тестовый сервер";
            }
            else if (SqlInstance.Equals("SQL2019"))
            {
                PublishType = ShareEnums.PublishType.Dev;
                PublishDescription = "Сервер разработки";
            }
            else if (SqlInstance.Equals("LUTON"))
            {
                PublishType = ShareEnums.PublishType.Release;
                PublishDescription = "Продуктовый сервер";
            }

            TaskTypes = new List<TaskTypeDirect>();
            Tasks = new List<TaskDirect>();
        }

        public void SetupTasks(int? scaleId)
        {
            if (scaleId == null)
                return;

            TaskTypes = TasksTypeUtils.GetTasksTypes();

            Tasks = new List<TaskDirect>();
            foreach (TaskTypeDirect taskType in TaskTypes)
            {
                TaskDirect task = TasksUtils.GetTask(taskType.Uid, (int)scaleId);
                if (task == null)
                {
                    TasksUtils.SaveTask(task, taskType, (int)scaleId, true);
                    task = TasksUtils.GetTask(taskType.Uid, (int)scaleId);
                }
                Tasks.Add(task);
            }
        }

        public bool IsTaskEnabled(ProjectsEnums.TaskType taskType)
        {
            if (Tasks == null)
                return false;
            // Table [TASKS] dont has records.
            if (Tasks.Count == 0)
                return true;

            foreach (TaskDirect task in Tasks)
            {
                if (string.Equals(task.TaskType.Name, taskType.ToString(), System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return task.Enabled;
                }
            }
            return false;
        }

        #endregion

        #region Public and private fields and properties

        private ShareEnums.PublishType _publishType;
        public ShareEnums.PublishType PublishType
        {
            get => _publishType;
            private set
            {
                _publishType = value;
                OnPropertyChanged();
            }
        }
        private string _publishDescription;
        public string PublishDescription
        {
            get => _publishDescription;
            private set
            {
                _publishDescription = value;
                OnPropertyChanged();
            }
        }
        private string _sqlInstance;
        public string SqlInstance
        {
            get => _sqlInstance;
            private set
            {
                _sqlInstance = value;
                OnPropertyChanged();
            }
        }
        private string _dataSource;
        public string DataSource
        {
            get => _dataSource;
            private set
            {
                _dataSource = value;
                OnPropertyChanged();
            }
        }
        private string _dataBase;
        public string DataBase
        {
            get => _dataBase;
            private set
            {
                _dataBase = value;
                OnPropertyChanged();
            }
        }
        private string _host;
        public string Host
        {
            get => _host;
            private set
            {
                _host = value;
                OnPropertyChanged();
            }
        }
        private List<TaskDirect> tasks;
        public List<TaskDirect> Tasks
        {
            get => tasks;
            private set
            {
                tasks = value;
                OnPropertyChanged();
            }
        }
        private List<TaskTypeDirect> _taskTypes;
        public List<TaskTypeDirect> TaskTypes
        {
            get => _taskTypes;
            private set
            {
                _taskTypes = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public and private methods

        private string GetInstance()
        {
            string result = string.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbSystem.Properties.GetInstance))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<string>(reader, "InstanceName");
                            }
                        }
                        reader.Close();
                    }
                    DataSource = con.DataSource;
                    DataBase = con.Database;
                    Host = con.WorkstationId;
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}
