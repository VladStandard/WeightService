// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataBaseCore.DAL.TableModels;
using DataBaseCore.DAL.Utils;
using DataBaseCore.Utils;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace DataBaseCore.DAL
{
    public class SqlHelper : INotifyPropertyChanged
    {
        #region Design pattern "Lazy Singleton"

        private static SqlHelper _instance;
        public static SqlHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyRaised([CallerMemberName] string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }

        #endregion

        #region Constructor and destructor

        public SqlHelper() { SetupDefault(); }

        public void SetupDefault()
        {
            DataSource = string.Empty;
            DataBase = string.Empty;
            Host = string.Empty;
            PublishType = EnumPublishType.Default;
            PublishDescription = "Неизвестный сервер";

            SqlInstance = GetInstance();
            if (SqlInstance.Equals("INS1"))
            {
                PublishType = EnumPublishType.Debug;
                PublishDescription = "Тестовый сервер";
            }
            else if (SqlInstance.Equals("SQL2019"))
            {
                PublishType = EnumPublishType.Dev;
                PublishDescription = "Сервер разработки";
            }
            else if (SqlInstance.Equals("LUTON"))
            {
                PublishType = EnumPublishType.Release;
                PublishDescription = "Продуктовый сервер";
            }

            TaskTypes = new List<TaskTypeEntity>();
            Tasks = new List<TaskEntity>();
        }

        public void SetupTasks(int? scaleId)
        {
            if (scaleId == null)
                return;

            TaskTypes = TasksTypeUtils.GetTasksTypes();

            Tasks = new List<TaskEntity>();
            foreach (TaskTypeEntity taskType in TaskTypes)
            {
                TaskEntity task = TasksUtils.GetTask(taskType.Uid, (int)scaleId);
                if (task == null)
                {
                    TasksUtils.SaveTask(task, taskType, (int)scaleId, true);
                    task = TasksUtils.GetTask(taskType.Uid, (int)scaleId);
                }
                Tasks.Add(task);
            }
        }

        public bool IsTaskEnabled(string taskName)
        {
            if (Tasks == null)
                return false;
            // Table [TASKS] dont has records.
            if (Tasks.Count == 0)
                return true;

            foreach (TaskEntity task in Tasks)
            {
                if (string.Equals(task.TaskType.Name, taskName, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return task.Enabled;
                }
            }
            return false;
        }

        #endregion

        #region Public and private fields and properties

        private EnumPublishType _publishType;
        public EnumPublishType PublishType
        {
            get => _publishType;
            private set
            {
                _publishType = value;
                OnPropertyRaised();
            }
        }
        private string _publishDescription;
        public string PublishDescription
        {
            get => _publishDescription;
            private set
            {
                _publishDescription = value;
                OnPropertyRaised();
            }
        }
        private string _sqlInstance;
        public string SqlInstance
        {
            get => _sqlInstance;
            private set
            {
                _sqlInstance = value;
                OnPropertyRaised();
            }
        }
        private string _dataSource;
        public string DataSource
        {
            get => _dataSource;
            private set
            {
                _dataSource = value;
                OnPropertyRaised();
            }
        }
        private string _dataBase;
        public string DataBase
        {
            get => _dataBase;
            private set
            {
                _dataBase = value;
                OnPropertyRaised();
            }
        }
        private string _host;
        public string Host
        {
            get => _host;
            private set
            {
                _host = value;
                OnPropertyRaised();
            }
        }
        private List<TaskEntity> tasks;
        public List<TaskEntity> Tasks
        {
            get => tasks;
            private set
            {
                tasks = value;
                OnPropertyRaised();
            }
        }
        private List<TaskTypeEntity> _taskTypes;
        public List<TaskTypeEntity> TaskTypes
        {
            get => _taskTypes;
            private set
            {
                _taskTypes = value;
                OnPropertyRaised();
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
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetInstance))
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
