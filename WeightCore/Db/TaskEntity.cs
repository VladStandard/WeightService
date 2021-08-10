// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using WeightCore.Utils;

namespace WeightCore.Db
{
    [Serializable]
    public class TaskEntity : BaseEntity<TaskEntity>
    {
        #region Public and private fields and properties

        public Guid Uid { get; private set; }
        public Guid TaskTypeUid { get; private set; }
        public string TaskTypeName { get; private set; }
        public int ScaleId { get; private set; }
        public string ScaleName { get; private set; }
        public bool Enabled { get; private set; }

        #endregion

        #region Constructor and destructor

        public TaskEntity(string taskName, string scaleName, bool enabled)
        {
            TaskTypeName = taskName;
            ScaleName = scaleName;
            Enabled = enabled;

            TaskTypeUid = GetTaskTypeUid(taskName);
            ScaleId = GetScaleId(scaleName);
            Uid = SaveTask(TaskTypeUid, ScaleId, enabled);
            GetTask(TaskTypeUid, ScaleId);
        }

        #endregion

        #region Public and private methods

        public Guid SaveTask(Guid taskTypeUid, int scaleId, bool enabled)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                // Get task UID.
                using (SqlCommand cmd = new SqlCommand(SqlQueries.IsTaskExists))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                    }
                    reader.Close();
                }
                // Is UID exists.
                if (Equals(result, Guid.Empty))
                {
                    using (SqlCommand cmd = new SqlCommand(SqlQueries.AddTask))
                    {
                        cmd.Connection = con;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                        cmd.Parameters.AddWithValue("@scale_id", scaleId);
                        cmd.Parameters.AddWithValue("@enabled", enabled);
                        cmd.ExecuteNonQuery();
                    }
                }
                // Get task UID.
                using (SqlCommand cmd = new SqlCommand(SqlQueries.IsTaskExists))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                    }
                    reader.Close();
                }
                // Close.
                con.Close();
            }
            return result;
        }

        public Guid GetTaskUid(string taskName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref taskName, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskUid))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public Guid GetTaskTypeUid(string taskTypeName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref taskTypeName, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskTypeUid))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskTypeName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public int GetScaleId(string scaleName)
        {
            int result = 0;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                UtilsString.StringValueTrim(ref scaleName, 150);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetScaleId))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@scale", scaleName);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = SqlConnectFactory.GetValue<int>(reader, "ID");
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public void GetTask(Guid taskTypeUid, int scaleId)
        {
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTask))
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        TaskTypeUid = SqlConnectFactory.GetValue<Guid>(reader, "TASK_TYPE_UID");
                        Enabled = SqlConnectFactory.GetValue<bool>(reader, "ENABLED");
                    }
                    reader.Close();
                }
                con.Close();
            }
        }

        #endregion
    }
}