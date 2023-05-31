// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.TableScaleModels.Templates;

namespace DeviceControl.Pages.Menu.References.SectionTemplates;

public sealed partial class ItemTemplate : RazorComponentItemBase<WsSqlTemplateModel>
{
    #region Public and private fields, properties, constructor

    private List<WsEnumTypeModel<string>> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories = BlazorAppSettings.DataSourceDics.GetTemplateCategories();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlTemplateModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlTemplateModel>();
    }

    #endregion
}