// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// SQL-контроллер таблицы Templates.
/// Клиентский слой доступа к БД.
/// </summary>
public sealed class WsSqlTemplateRepository : WsSqlTableRepositoryBase<WsSqlTemplateModel>
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlTemplateRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlTemplateRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private methods

    public WsSqlTemplateModel GetNewItem() => SqlCore.GetItemNewEmpty<WsSqlTemplateModel>();

    public WsSqlTemplateModel GetItem(ushort pluNumber)
    {
        WsSqlViewPluLineModel viewPluScale = ContextCache.LocalViewPlusLines.Find(item => 
            Equals(item.PluNumber, pluNumber));
        return SqlCore.GetItemNotNullableByUid<WsSqlTemplateModel>(viewPluScale.Identity.Uid);
    }

    public List<WsSqlTemplateModel> GetList() => ContextList.GetListNotNullableTemplates(SqlCrudConfig);

    #endregion
}