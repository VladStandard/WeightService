// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private methods

	public static long GetScaleId(string scaleDescription)
	{
		long result = 0;
		using SqlConnection con = SqlConnect.GetConnection();
		con.Open();
		StringUtils.SetStringValueTrim(ref scaleDescription, 150);
		using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleId))
		{
			cmd.Connection = con;
			cmd.Parameters.Clear();
			cmd.Parameters.AddWithValue("@SCALE_DESCRIPTION", scaleDescription);
			using SqlDataReader reader = cmd.ExecuteReader();
			if (reader.HasRows)
			{
				if (reader.Read())
				{
					result = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
				}
			}
			reader.Close();
		}
		con.Close();
		return result;
	}

	public static ScaleModel? GetScaleFromHost(long hostId)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new($"Host.Identity.Id", SqlFieldComparerEnum.Equal, hostId), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
			new(SqlFieldEnum.CreateDt, SqlFieldOrderDirectionEnum.Desc), 0);
		return DataAccess.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ScaleModel? GetScale(long id)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(SqlFieldEnum.IdentityValueId, SqlFieldComparerEnum.Equal, id), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
			null, 0);
		return DataAccess.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ScaleModel? GetScale(string description)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(SqlFieldEnum.Description, SqlFieldComparerEnum.Equal, description), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
			null, 0);
		return DataAccess.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ProductionFacilityModel? GetArea(string name)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(SqlFieldEnum.Name, SqlFieldComparerEnum.Equal, name), new(SqlFieldEnum.IsMarked, SqlFieldComparerEnum.Equal, false) },
			null, 0);
		return DataAccess.GetItem<ProductionFacilityModel>(sqlCrudConfig);
	}

	#endregion
}
