using Ws.StorageCore.Common;
using Ws.StorageCore.Models;
using Ws.StorageCore.OrmUtils;
using Ws.StorageCore.Utils;
namespace Ws.StorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Barcode helper.
/// </summary>
public sealed class SqlAppRepository : SqlTableRepositoryBase<SqlAppEntity>
{
    #region Items

    public SqlAppEntity GetItemByName(string appName)
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigFactory.GetCrudAll();
        sqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(SqlEntityBase.Name), appName));
        return SqlCore.GetItemByCrud<SqlAppEntity>(sqlCrudConfig);
    }

    public SqlAppEntity GetItemByUid(Guid uid) => SqlCore.GetItemByUid<SqlAppEntity>(uid);

    public SqlAppEntity GetItemByNameOrCreate(string appName)
    {
        SqlAppEntity access = GetItemByName(appName);
        if (access.IsNew) 
            access.Name = appName;
        return SaveOrUpdate(access);
    }
    
    public SqlAppEntity SaveOrUpdate (SqlAppEntity accessEntity)
    {
        // TODO: add Access validator
        if (!accessEntity.IsNew)
            SqlCore.Update(accessEntity);
        else 
            SqlCore.Save(accessEntity);
        return accessEntity;
    }
   
    public SqlAppEntity GetNewItem()
    {
        SqlAppEntity app = SqlCore.GetItemNewEmpty<SqlAppEntity>();
        //app.Name = app.DisplayName;
        return app;
    }

    #endregion

    #region List

    public List<SqlAppEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.NameAsc());
        return SqlCore.GetEnumerable<SqlAppEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}