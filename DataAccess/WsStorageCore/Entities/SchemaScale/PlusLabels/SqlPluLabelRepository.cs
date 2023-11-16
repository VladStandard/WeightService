namespace WsStorageCore.Entities.SchemaScale.PlusLabels;

/// <summary>
/// Репозиторий таблицы PLUS_LABELS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class SqlPluLabelRepository : SqlTableRepositoryBase<SqlPluLabelEntity>
{

    #region Public and private methods

    /// <summary>
    /// Сохранить этикетку ПЛУ.
    /// </summary>
    public void Save(SqlPluLabelEntity pluLabel)
    {
        if (!pluLabel.PluScale.Plu.IsCheckWeight)
            pluLabel.PluWeighing = null;
        SqlCore.Save(pluLabel);
    }

    public List<SqlPluLabelEntity> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<SqlPluLabelEntity>(sqlCrudConfig).ToList();
    }

    #endregion
}