using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaRef.Hosts;

public sealed class WsSqlHostRepository : WsSqlTableRepositoryBase<WsSqlHostEntity>
{
    #region Public and private methods

    public WsSqlHostEntity GetItemByName(string name)
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlEntityBase.Name), name));
        return SqlCore.GetItemByCrud<WsSqlHostEntity>(sqlCrudConfig);
    }
    
    public WsSqlHostEntity SaveOrUpdate (WsSqlHostEntity hostEntity)
    {
        hostEntity.LoginDt = DateTime.Now;
        if (!hostEntity.IsNew)
            SqlCore.Update(hostEntity);
        else
        {
            hostEntity.LoginDt = DateTime.Now;
            SqlCore.Save(hostEntity);
        }
        return hostEntity;
    }
    
    public WsSqlHostEntity GetItemByNameOrCreate(string name)
    {
        WsSqlHostEntity host = GetItemByName(name);
        if (host.IsNew)
        {
            host.Name = name;
            host.Ip = MdNetUtils.GetLocalIpAddress();
        }
        return SaveOrUpdate(host);
    }
    
    public WsSqlHostEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlHostEntity>();

    public WsSqlHostEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<WsSqlHostEntity>(uid);
    
    public IEnumerable<WsSqlHostEntity> GetEnumerable(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<WsSqlHostEntity>(sqlCrudConfig);
    }

    #endregion
}