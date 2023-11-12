namespace WsStorageCore.Entities.SchemaScale.PlusLabels;

/// <summary>
/// Репозиторий таблицы PLUS_LABELS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLabelRepository : WsSqlTableRepositoryBase<WsSqlPluLabelEntity>
{

    #region Public and private methods

    /// <summary>
    /// Сохранить этикетку ПЛУ.
    /// </summary>
    public void Save(WsSqlPluLabelEntity pluLabel)
    {
        if (!pluLabel.PluScale.Plu.IsCheckWeight)
            pluLabel.PluWeighing = null;
        SqlCore.Save(pluLabel);
    }

    public List<WsSqlPluLabelEntity> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlPluLabelEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}