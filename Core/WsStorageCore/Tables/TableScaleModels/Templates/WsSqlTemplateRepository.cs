namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// SQL-контроллер таблицы Templates.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlTemplateRepository : WsSqlTableRepositoryBase<WsSqlTemplateModel>
{
    #region Public and private methods

    public WsSqlTemplateModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlTemplateModel>();
    
    public List<WsSqlTemplateModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(nameof(WsSqlTemplateModel.Title));
        return SqlCore.GetEnumerableNotNullable<WsSqlTemplateModel>(sqlCrudConfig).ToList();
    }
    
    #endregion
}