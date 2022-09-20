// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation.Results;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace DataCore.Sql.Core;

public static partial class SqlUtils
{
	#region Public and private fields and properties

	public static SqlConnectFactory SqlConnect { get; } = SqlConnectFactory.Instance;
	public static DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
	public static readonly string FilePathToken = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.xml";
	public static readonly string FilePathLog = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\scalesui.log";

	#endregion

	#region Public and private methods

	public static SqlCrudConfigModel GetCrudConfigIsMarked() => GetCrudConfig(null, null, 0, false, false);

	public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel>? filters, SqlFieldOrderModel? order, int maxResults, 
        bool isShowMarked, bool isShowOnlyTop)
	{
		maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;
		SqlCrudConfigModel sqlCrudConfig = new(filters, order, maxResults);
		List<SqlFieldFilterModel> filtersMarked = new() { new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, false) };
		if (!isShowMarked)
		{
			switch (sqlCrudConfig.Filters)
			{
				case null:
					sqlCrudConfig.Filters = filtersMarked;
					break;
				default:
					sqlCrudConfig.Filters.AddRange(filtersMarked);
					break;
			}
		}

		return sqlCrudConfig;
	}

	//public static SqlTableScaleEnum GetTableScale(string tableName)
	//{
	//	if (Enum.TryParse(tableName, out SqlTableScaleEnum tableScale))
	//		return tableScale;
	//	return SqlTableScaleEnum.Empty;
	//}

	#endregion
}
