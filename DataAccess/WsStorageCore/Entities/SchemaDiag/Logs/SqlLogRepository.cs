using WsStorageCore.Enums;

namespace WsStorageCore.Entities.SchemaDiag.Logs;

public class SqlLogRepository : SqlTableRepositoryBase<SqlLogEntity>
{
    public SqlLogEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<SqlLogEntity>(uid);

    public SqlLogEntity GetItemFirst() => SqlCore.GetItemFirst<SqlLogEntity>();

    public IEnumerable<SqlLogEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlLogEntity>(sqlCrudConfig);
    }

    public IList<SqlLogEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetList<SqlLogEntity>(sqlCrudConfig);
    }
    public IEnumerable<SqlLogEntity> GetListByLogTypeAndLineName(SqlCrudConfigModel sqlCrudConfig, LogTypeEnum? logType, SqlScaleEntity? line)
    {
        sqlCrudConfig.IsReadUncommitted = true;
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        
        sqlCrudConfig.ClearFilters();
   
        if (line is not null)
        {
            sqlCrudConfig.AddFilter(SqlRestrictions.EqualFk(nameof(SqlLogEntity.Device), line.Host));
        }
        if (logType is not null)
        {
            sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlLogEntity.Type), logType));
        }
       
        return SqlCore.GetList<SqlLogEntity>(sqlCrudConfig);
    }
}