// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private methods

	//public static long GetScaleId(string scaleDescription)
	//{
	//	long result = 0;
	//	using SqlConnection con = SqlConnect.GetConnection();
	//	con.Open();
	//	StringUtils.SetStringValueTrim(ref scaleDescription, 150);
	//	using (SqlCommand cmd = new(SqlQueries.DbScales.Tables.Scales.GetScaleId))
	//	{
	//		cmd.Connection = con;
	//		cmd.Parameters.Clear();
	//		cmd.Parameters.AddWithValue("@SCALE_DESCRIPTION", scaleDescription);
	//		using SqlDataReader reader = cmd.ExecuteReader();
	//		if (reader.HasRows)
	//		{
	//			if (reader.Read())
	//			{
	//				result = SqlConnect.GetValueAsNotNullable<long>(reader, "ID");
	//			}
	//		}
	//		reader.Close();
	//	}
	//	con.Close();
	//	return result;
	//}

	//public static ScaleModel? GetScaleFromDevice(DeviceModel deviceModel)
	//{
	//	SqlCrudConfigModel sqlCrudConfig = new(
	//           new List<SqlFieldFilterModel>
	//           { 
	//               new($"{nameof(DeviceModel)}.{nameof(SqlTableBase.IdentityValueUid)}", 
	//                SqlFieldComparerEnum.Equal, deviceModel.IdentityValueUid), 
	//               new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false)
	//           },
	//		new SqlFieldOrderModel(nameof(SqlTableBase.CreateDt), SqlFieldOrderEnum.Desc), 0);
	//	DeviceScaleFkModel? deviceScaleFk = DataAccess.GetItem<DeviceScaleFkModel>(sqlCrudConfig);
	//	return deviceScaleFk?.Scale;
	//}

	//public static ScaleModel GetScaleFromDeviceNotNullable(DeviceModel device)
	//{
	//	ScaleModel? scale = GetScaleFromDevice(device);
	//	return scale ?? new();
	//}

	//public static ScaleModel? GetScale(long id)
	//{
	//	SqlCrudConfigModel sqlCrudConfig = new(
	//		new List<SqlFieldFilterModel>()
	//		{
	//			new(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id),
	//			new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false)
	//		});
	//	return DataAccess.GetItem<ScaleModel>(sqlCrudConfig);
	//}

	public static ScaleModel GetScaleNotNullable(long id)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
            new List<SqlFieldFilterModel>()
            {
                new(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, id), 
                new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false)
            }, false, false);
		return DataAccessHelper.Instance.GetItemNotNullable<ScaleModel>(sqlCrudConfig);
	}

	public static ProductionFacilityModel GetAreaNotNullable(string name)
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(
			new List<SqlFieldFilterModel>()
            {
                new(nameof(ProductionFacilityModel.Name), SqlFieldComparerEnum.Equal, name), 
                new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false)
            }, false, false);
		return DataAccessHelper.Instance.GetItemNotNullable<ProductionFacilityModel>(sqlCrudConfig);
	}

	#endregion
}
