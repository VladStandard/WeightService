// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private methods

	//public static void SaveNullTask(TaskTypeDirect taskType, long scaleId, bool enabled)
	//{
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.InsertTask);
	//	cmd.Connection = con;
	//	cmd.Parameters.Clear();
	//	cmd.Parameters.AddWithValue("@task_type_uid", taskType.Uid);
	//	cmd.Parameters.AddWithValue("@scale_id", scaleId);
	//	cmd.Parameters.AddWithValue("@enabled", enabled);
	//	cmd.ExecuteNonQuery();
	//	con.Close();
	//}

	//public static void SaveTask(TaskDirect task, bool enabled)
	//{
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.UpdateTask);
	//	cmd.Connection = con;
	//	cmd.Parameters.Clear();
	//	cmd.Parameters.AddWithValue("@uid", task.Uid);
	//	cmd.Parameters.AddWithValue("@enabled", enabled);
	//	cmd.ExecuteNonQuery();
	//	con.Close();
	//}

	//public static Guid GetTaskUid(string taskName)
	//{
	//	Guid result = Guid.Empty;
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	StringUtils.SetStringValueTrim(ref taskName, 32);
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskUid))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_type", taskName);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static TaskDirect? GetTask(Guid taskTypeUid, long scaleId)
	//{
	//	TaskDirect? result = null;
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByTypeAndScale))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_type_uid", taskTypeUid);
	//		cmd.Parameters.AddWithValue("@scale_id", scaleId);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result = new()
	//				{
	//					Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
	//					TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
	//					//Scale = ScalesUtils.GetScale(dataAccess, SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
	//					Scale = DataAccess.Crud.GetItemById<ScaleModel>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
	//					Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
	//				};
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static TaskDirect? GetTask(Guid taskUid)
	//{
	//	TaskDirect? result = null;
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Tasks.GetTaskByUid))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_uid", taskUid);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result = new()
	//				{
	//					Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID"),
	//					TaskType = GetTaskType(SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")),
	//					//Scale = ScalesUtils.GetScale(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
	//					Scale = DataAccess.Crud.GetItemById<ScaleModel>(SqlConnect.GetValueAsNotNullable<int>(reader, "SCALE_ID")),
	//					Enabled = SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")
	//				};
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static Guid GetTaskTypeUid(string taskTypeName)
	//{
	//	Guid result = Guid.Empty;
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	StringUtils.SetStringValueTrim(ref taskTypeName, 32);
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTaskTypeUid))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_type", taskTypeName);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static TaskTypeDirect GetTaskType(string name)
	//{
	//	TaskTypeDirect result = new();
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByName))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_name", name);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
	//				result.Name = SqlConnect.GetValueAsString(reader, "NAME");
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static TaskTypeDirect GetTaskType(Guid uid)
	//{
	//	TaskTypeDirect result = new();
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypesByUid))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@task_uid", uid);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result.Uid = SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID");
	//				result.Name = SqlConnect.GetValueAsString(reader, "NAME");
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static List<TaskTypeDirect> GetTasksTypes()
	//{
	//	List<TaskTypeDirect> result = new();
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypes))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			while (reader.Read())
	//			{
	//				result.Add(new(
	//				 SqlConnect.GetValueAsNotNullable<Guid>(reader, "UID"), SqlConnect.GetValueAsString(reader, "NAME")));
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	#endregion
}
