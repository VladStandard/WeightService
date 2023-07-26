// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.PlusLabels;

/// <summary>
/// Репозиторий таблицы PLUS_LABELS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLabelRepository : WsSqlTableRepositoryBase<WsSqlPluLabelModel>
{

    #region Public and private methods

    //public WsSqlPluLabelModel GetNewItem()
    //{
    //    WsSqlPluLabelModel item = SqlCore.GetItemNewEmpty<WsSqlPluLabelModel>();
    //    item.Plu = ContextPlu.GetNewItem();
    //    item.Parent = ContextPlu.GetNewItem();
    //    item.Category = null;
    //    return item;
    //}

    //public List<WsSqlPluLabelModel> GetList() => ContextList.GetListNotNullablePlusFks(SqlCrudConfig);

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

    public List<WsSqlPluLabelModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) {
        if (sqlCrudConfig.IsResultOrder)
            sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlTableBase.ChangeDt), Direction = WsSqlEnumOrder.Desc });
        return SqlCore.GetListNotNullable<WsSqlPluLabelModel>(sqlCrudConfig);
    }

    #endregion
}