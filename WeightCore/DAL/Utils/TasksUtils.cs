// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Data.SqlClient;
using WeightCore.DAL.TableModels;
using WeightCore.Utils;

namespace WeightCore.DAL.Utils
{
    public static class TasksUtils
    {
        #region Public and private methods

        public static Guid SaveTask(Guid taskTypeUid, int scaleId, bool enabled)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                // Get task UID.
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskByTypeAndScale))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<Guid>(reader, "TASK_UID");
                            }
                        }
                        reader.Close();
                    }
                }
                // Task is not exists.
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
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskByTypeAndScale))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<Guid>(reader, "TASK_UID");
                            }
                        }
                        reader.Close();
                    }
                }
                // Close.
                con.Close();
            }
            return result;
        }

        public static Guid GetTaskUid(string taskName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                StringUtils.StringValueTrim(ref taskName, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static TaskEntity GetTask(Guid taskTypeUid, int scaleId)
        {
            TaskEntity result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskByTypeAndScale))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = new TaskEntity
                                {
                                    Uid = SqlConnectFactory.GetValue<Guid>(reader, "TASK_UID"),
                                    TaskType = TasksTypeUtils.GetTaskType(SqlConnectFactory.GetValue<Guid>(reader, "TASK_TYPE_UID")),
                                    Scale = ScalesUtils.GetScale(SqlConnectFactory.GetValue<int>(reader, "SCALE_ID")),
                                    Enabled = SqlConnectFactory.GetValue<bool>(reader, "ENABLED")
                                };
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static TaskEntity GetTask(Guid taskUid)
        {
            TaskEntity result = null;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_uid", taskUid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result = new TaskEntity
                                {
                                    Uid = SqlConnectFactory.GetValue<Guid>(reader, "TASK_UID"),
                                    TaskType = TasksTypeUtils.GetTaskType(SqlConnectFactory.GetValue<Guid>(reader, "TASK_TYPE_UID")),
                                    Scale = ScalesUtils.GetScale(SqlConnectFactory.GetValue<int>(reader, "SCALE_ID")),
                                    Enabled = SqlConnectFactory.GetValue<bool>(reader, "ENABLED")
                                };
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}