using WsStorageCore.Enums;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaDiag.Logs;

public class WsSqlLogRepository : WsSqlTableRepositoryBase<WsSqlLogEntity>
{
    public WsSqlLogEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlLogEntity>(uid);

    public WsSqlLogEntity GetItemFirst() => SqlCore.GetItemFirst<WsSqlLogEntity>();

    public IEnumerable<WsSqlLogEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlLogEntity>(sqlCrudConfig);
    }

    public IList<WsSqlLogEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetList<WsSqlLogEntity>(sqlCrudConfig);
    }
    public IEnumerable<WsSqlLogEntity> GetListByLogTypeAndLineName(WsSqlCrudConfigModel sqlCrudConfig, LogTypeEnum? logType, WsSqlScaleEntity? line)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        
        sqlCrudConfig.ClearFilters();
   
        if (line is not null)
        {
            sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(WsSqlLogEntity.Device), line.Host));
        }
        if (logType is not null)
        {
            sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlLogEntity.Type), logType));
        }
       
        return SqlCore.GetList<WsSqlLogEntity>(sqlCrudConfig);
    }
}