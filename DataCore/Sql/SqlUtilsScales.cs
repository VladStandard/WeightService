// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql;

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
				{ new($"Host.Identity.Id", ShareEnums.DbComparer.Equal, hostId), new(ShareEnums.DbField.IsMarked, ShareEnums.DbComparer.Equal, false) },
			new(ShareEnums.DbField.CreateDt, ShareEnums.DbOrderDirection.Desc), 0);
		return DataAccess.Crud.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ScaleModel? GetScale(long id)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(ShareEnums.DbField.IdentityId, ShareEnums.DbComparer.Equal, id), new(ShareEnums.DbField.IsMarked, ShareEnums.DbComparer.Equal, false) },
			null, 0);
		return DataAccess.Crud.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ScaleModel? GetScale(string description)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(ShareEnums.DbField.Description, ShareEnums.DbComparer.Equal, description), new(ShareEnums.DbField.IsMarked, ShareEnums.DbComparer.Equal, false) },
			null, 0);
		return DataAccess.Crud.GetItem<ScaleModel>(sqlCrudConfig);
	}

	public static ProductionFacilityModel? GetArea(string name)
	{
		SqlCrudConfigModel sqlCrudConfig = new(new()
				{ new(ShareEnums.DbField.Name, ShareEnums.DbComparer.Equal, name), new(ShareEnums.DbField.IsMarked, ShareEnums.DbComparer.Equal, false) },
			null, 0);
		return DataAccess.Crud.GetItem<ProductionFacilityModel>(sqlCrudConfig);
	}

	#endregion
}
