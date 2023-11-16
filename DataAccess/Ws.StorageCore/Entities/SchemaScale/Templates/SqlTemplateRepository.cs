namespace Ws.StorageCore.Entities.SchemaScale.Templates;

/// <summary>
/// SQL-контроллер таблицы Templates.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlTemplateRepository : SqlTableRepositoryBase<SqlTemplateEntity>
{
    #region Public and private methods

    public SqlTemplateEntity GetNewItem() => SqlCore.GetItemNewEmpty<SqlTemplateEntity>();
    
    public List<SqlTemplateEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.Asc(nameof(SqlTemplateEntity.Title)));
        return SqlCore.GetEnumerable<SqlTemplateEntity>(sqlCrudConfig).ToList();
    }
    
    #endregion
}