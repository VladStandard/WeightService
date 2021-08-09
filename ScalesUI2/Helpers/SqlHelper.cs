using WeightCore.Db;
using ScalesUI.Utils;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Threading;

namespace ScalesUI.Helpers
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

            TypePublish = EnumTypePublish.Default;
            SqlInstance = GetSqlInstance();
            if (SqlInstance.Equals("INS1"))
            {
                TypePublish = EnumTypePublish.Debug;
            }
            else if (SqlInstance.Equals("SQL2019"))
            {
                TypePublish = EnumTypePublish.Dev;
            }
            else if (SqlInstance.Equals("LUTON"))
            {
                TypePublish = EnumTypePublish.Release;
            }
        }

        #endregion

        #region Public and private fields and properties

        private EnumTypePublish _typePublish;
        public EnumTypePublish TypePublish
        {
            get => _typePublish;
            set
            {
                _typePublish = value;
                OnPropertyRaised();
            }
        }
        private string _sqlInstance;
        public string SqlInstance
        {
            get => _sqlInstance;
            set
            {
                _sqlInstance = value;
                OnPropertyRaised();
            }
        }
        private string _dataSource;
        public string DataSource
        {
            get => _dataSource;
            set
            {
                _dataSource = value;
                OnPropertyRaised();
            }
        }
        private string _dataBase;
        public string DataBase
        {
            get => _dataBase;
            set
            {
                _dataBase = value;
                OnPropertyRaised();
            }
        }
        private string _host;
        public string Host
        {
            get => _host;
            set
            {
                _host = value;
                OnPropertyRaised();
            }
        }

        #endregion

        #region Public and private methods

        private SqlCommand GetSqlInstanceCmd(SqlConnection con)
        {
            string query = @"select serverproperty('InstanceName') [InstanceName]";
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
                SqlCommand cmd = GetSqlInstanceCmd(con);
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
