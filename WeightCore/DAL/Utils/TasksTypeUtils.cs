// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WeightCore.DAL.TableModels;
using WeightCore.Utils;

namespace WeightCore.DAL.Utils
{
    public static class TasksTypeUtils
    {
        #region Public and private methods

        public static Guid GetTaskTypeUid(string taskTypeName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                StringUtils.StringValueTrim(ref taskTypeName, 32);
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTaskTypeUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskTypeName);
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

        public static TaskTypeEntity GetTaskType(string name)
        {
            TaskTypeEntity result = new TaskTypeEntity();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTasksTypesByName))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_name", name);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result.Uid = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                                result.Name = SqlConnectFactory.GetValue<string>(reader, "NAME");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static TaskTypeEntity GetTaskType(Guid uid)
        {
            TaskTypeEntity result = new TaskTypeEntity();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTasksTypesByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_uid", uid);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            if (reader.Read())
                            {
                                result.Uid = SqlConnectFactory.GetValue<Guid>(reader, "UID");
                                result.Name = SqlConnectFactory.GetValue<string>(reader, "NAME");
                            }
                        }
                        reader.Close();
                    }
                }
                con.Close();
            }
            return result;
        }

        public static List<TaskTypeEntity> GetTasksTypes()
        {
            List<TaskTypeEntity> result = new List<TaskTypeEntity>();
            using (SqlConnection con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(SqlQueries.GetTasksTypes))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                result.Add(new TaskTypeEntity(
                                    SqlConnectFactory.GetValue<Guid>(reader, "UID"), SqlConnectFactory.GetValue<string>(reader, "NAME")));
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