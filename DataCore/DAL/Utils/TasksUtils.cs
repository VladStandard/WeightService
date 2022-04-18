// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.Models;
using DataCore.DAL.TableDirectModels;
using DataCore.Utils;
using Microsoft.Data.SqlClient;
using System;

namespace DataCore.DAL.Utils
{
    public static class TasksUtils
    {
        #region Public and private fields and properties

        public static SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

        #endregion

        #region Public and private methods

        public static void SaveNullTask(TaskTypeDirect taskType, long scaleId, bool enabled)
        {
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.InsertTask);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@task_type_uid", taskType.Uid);
            cmd.Parameters.AddWithValue("@scale_id", scaleId);
            cmd.Parameters.AddWithValue("@enabled", enabled);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static void SaveTask(TaskDirect task, bool enabled)
        {
            if (task == null)
                return;
            using SqlConnection con = SqlConnect.GetConnection();
            con.Open();
            using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.UpdateTask);
            cmd.Connection = con;
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@uid", task.Uid);
            cmd.Parameters.AddWithValue("@enabled", enabled);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public static Guid GetTaskUid(string taskName)
        {
            Guid result = Guid.Empty;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                StringUtils.SetStringValueTrim(ref taskName, 32);
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type", taskName);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskDirect? GetTask(DataAccessEntity dataAccess, Guid taskTypeUid, long scaleId)
        {
            TaskDirect? result = null;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByTypeAndScale))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
                    cmd.Parameters.AddWithValue("@scale_id", scaleId);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = new TaskDirect
                            {
                                Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
                                TaskType = TasksTypeUtils.GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
                                //Scale = ScalesUtils.GetScale(dataAccess, SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Scale = dataAccess.Crud.GetEntity<TableScaleModels.ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
                            };
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        public static TaskDirect? GetTask(DataAccessEntity dataAccess, Guid taskUid)
        {
            TaskDirect? result = null;
            using (SqlConnection con = SqlConnect.GetConnection())
            {
                con.Open();
                using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByUid))
                {
                    cmd.Connection = con;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@task_uid", taskUid);
                    using SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            result = new TaskDirect
                            {
                                Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
                                TaskType = TasksTypeUtils.GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
                                //Scale = ScalesUtils.GetScale(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Scale = dataAccess.Crud.GetEntity<TableScaleModels.ScaleEntity>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
                                Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
                            };
                        }
                    }
                    reader.Close();
                }
                con.Close();
            }
            return result;
        }

        #endregion
    }
}