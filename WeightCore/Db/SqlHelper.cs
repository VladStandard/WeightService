using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;
using WeightCore.Utils;

namespace WeightCore.Db
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

            SqlInstance = GetSqlInstance();
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

            TaskNames = new List<string>() {
                "DeviceManager",
                "MassaManager",
                "MemoryManager",
                "PrintManager",
            };
            TaskItems = new List<TaskEntity>();
        }

        public void SetupTasks(string scaleName)
        {
            TaskItems = new List<TaskEntity>();
            foreach (var name in TaskNames)
            {
                TaskItems.Add(new TaskEntity(name, scaleName, true));
            }
        }

        public bool IsTaskEnabled(string taskName)
        {
            foreach (var item in TaskItems)
            {
                if (string.Equals(item.TaskTypeName, taskName, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    return item.Enabled;
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
        private List<TaskEntity> taskItems;
        public List<TaskEntity> TaskItems
        {
            get => taskItems;
            private set
            {
                taskItems = value;
                OnPropertyRaised();
            }
        }
        private List<string> _taskNames;
        public List<string> TaskNames
        {
            get => _taskNames;
            private set
            {
                _taskNames = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public and private methods

        public SqlCommand GetCmd(SqlConnection con, string query)
        {
            SqlCommand cmd = new SqlCommand(query, con) { CommandType = CommandType.Text };
            cmd.Prepare();
            return cmd;
        }

        private string GetSqlInstance()
        {
            string result = string.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                SqlCommand cmd = GetCmd(con, SqlQueries.GetInstance);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<string>(reader, "InstanceName");
                    }
                }
                reader.Close();
                DataSource = con.DataSource;
                DataBase = con.Database;
                Host = con.WorkstationId;
            }
            return result;
        }

        #endregion
    }
}
