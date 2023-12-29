using System;

namespace Ws.StorageCore.Entities.SchemaRef.Hosts;

public sealed class SqlHostRepository : SqlTableRepositoryBase<SqlHostEntity>
{
    public SqlHostEntity GetItemByName(string name)
    {
        SqlCrudConfigModel sqlCrudConfig = new();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.Name), name));
        return SqlCore.GetItemByCrud<SqlHostEntity>(sqlCrudConfig);
    }
    
    public SqlHostEntity SaveOrUpdate(SqlHostEntity hostEntity)
    {
        if (!hostEntity.IsNew)
            SqlCore.Update(hostEntity);
        else
        {
            SqlCore.Save(hostEntity);
        }
        return hostEntity;
    }
    
    public SqlHostEntity GetItemByNameOrCreate(string name)
    {
        SqlHostEntity host = GetItemByName(name);

        if (host.IsNew) 
            host.Name = name;
        
        host.Ip = MdNetUtils.GetLocalIpAddress();
        host.LoginDt = DateTime.Now;
        
        return SaveOrUpdate(host);
    }
    
    public SqlHostEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlHostEntity>();

    public SqlHostEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<SqlHostEntity>(uid);
    
    public IEnumerable<SqlHostEntity> GetEnumerable(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlHostEntity>(sqlCrudConfig);
    }
}