using System;
using Ws.Shared.Enums;

namespace Ws.StorageCore.Entities.SchemaScale.Access;

public sealed class SqlAccessRepository : SqlTableRepositoryBase<SqlAccessEntity>
{
    #region Item
    
    public SqlAccessEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlAccessEntity>();

    public SqlAccessEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<SqlAccessEntity>(uid);

    public SqlAccessEntity GetItemByUsername(string userName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.Name), userName));
        return SqlCore.GetItemByCrud<SqlAccessEntity>(sqlCrudConfig);
    }
    
    public SqlAccessEntity GetItemByNameOrCreate(string username)
    {
        SqlAccessEntity access = GetItemByUsername(username);
        if (access.IsNew)
        {
            access.Name = username;
            access.Rights = (byte)EnumAccessRights.None;
        }
        return SaveOrUpdate(access);
    }
    
    public SqlAccessEntity SaveOrUpdate (SqlAccessEntity accessEntity)
    {
        // TODO: add Access validator
        accessEntity.LoginDt = DateTime.Now;
        if (!accessEntity.IsNew)
            SqlCore.Update(accessEntity);
        else 
            SqlCore.Save(accessEntity);
        return accessEntity;
    }
    
    #endregion

    #region List

    public List<SqlAccessEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlAccessEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}