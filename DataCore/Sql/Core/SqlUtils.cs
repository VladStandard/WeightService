// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

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

    public static List<SqlFieldFilterModel> GetFiltersIsMarked(bool isShowMarked) =>
        new() { new(nameof(SqlTableBase.IsMarked), SqlFieldComparerEnum.Equal, isShowMarked) };

    public static SqlCrudConfigModel GetCrudConfig(int maxResults, bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(new List<SqlFieldFilterModel>(), new List<SqlFieldOrderModel>(), maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldFilterModel filter, SqlFieldOrderModel order, 
        int maxResults, bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(new List<SqlFieldFilterModel> { filter }, new List<SqlFieldOrderModel> { order }, maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldFilterModel filter, int maxResults, bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(new List<SqlFieldFilterModel> { filter }, new List<SqlFieldOrderModel>(), maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldOrderModel order, 
        int maxResults, bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(new List<SqlFieldFilterModel>(), new List<SqlFieldOrderModel> { order }, maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(SqlFieldFilterModel filter, List<SqlFieldOrderModel> orders, int maxResults,
        bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(new List<SqlFieldFilterModel> { filter }, orders, maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, SqlFieldOrderModel order, int maxResults,
        bool isShowMarked, bool isShowOnlyTop) =>
        GetCrudConfig(filters, new List<SqlFieldOrderModel> { order }, maxResults, isShowMarked, isShowOnlyTop);

    public static SqlCrudConfigModel GetCrudConfig(List<SqlFieldFilterModel> filters, List<SqlFieldOrderModel> orders, 
        int maxResults, bool isShowMarked, bool isShowOnlyTop)
    {
        maxResults = isShowOnlyTop ? DataAccess.JsonSettingsLocal.SelectTopRowsCount : maxResults;
        SqlCrudConfigModel sqlCrudConfig = new(filters, orders, maxResults);
        switch (isShowMarked)
        {
            case false:
                switch (sqlCrudConfig.Filters)
                {
                    case null:
                        sqlCrudConfig.Filters = GetFiltersIsMarked(false);
                        break;
                    default:
                        sqlCrudConfig.Filters.AddRange(GetFiltersIsMarked(false));
                        break;
                }
                break;
        }
        return sqlCrudConfig;
    }

    #endregion
}
