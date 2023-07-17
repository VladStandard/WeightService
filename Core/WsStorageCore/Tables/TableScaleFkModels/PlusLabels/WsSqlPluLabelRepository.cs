// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusLabels;

/// <summary>
/// Репозиторий таблицы PLUS_LABELS.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlPluLabelRepository : WsSqlTableRepositoryBase<WsSqlPluLabelModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlPluLabelRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlPluLabelRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private WsSqlPluRepository ContextPlu => WsSqlPluRepository.Instance;

    #endregion

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

    #endregion
}