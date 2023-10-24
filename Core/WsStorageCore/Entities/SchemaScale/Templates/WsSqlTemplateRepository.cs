using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaScale.Templates;

/// <summary>
/// SQL-контроллер таблицы Templates.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlTemplateRepository : WsSqlTableRepositoryBase<WsSqlTemplateEntity>
{
    #region Public and private methods

    public WsSqlTemplateEntity GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlTemplateEntity>();
    
    public List<WsSqlTemplateEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(WsSqlTemplateEntity.Title)));
        return SqlCore.GetEnumerable<WsSqlTemplateEntity>(sqlCrudConfig).ToList();
    }
    
    #endregion
}