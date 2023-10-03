using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableScaleModels.PlusLabels;

/// <summary>
/// Репозиторий таблицы PLUS_LABELS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLabelRepository : WsSqlTableRepositoryBase<WsSqlPluLabelModel>
{

    #region Public and private methods

    /// <summary>
    /// Сохранить этикетку ПЛУ.
    /// </summary>
    /// <param name="pluLabel"></param>
    public void Save(WsSqlPluLabelModel pluLabel)
    {
        switch (WsDebugHelper.Instance.Config)
        {
            case WsEnumConfiguration.ReleaseAleksandrov:
            case WsEnumConfiguration.ReleaseMorozov:
            case WsEnumConfiguration.ReleaseVS:
                pluLabel.Xml = null;
                break;
        }
        SqlCore.Save(pluLabel);
    }

    public List<WsSqlPluLabelModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrder(SqlOrder.CreateDtDesc());
        return SqlCore.GetEnumerable<WsSqlPluLabelModel>(sqlCrudConfig).ToList();
    }

    #endregion
}