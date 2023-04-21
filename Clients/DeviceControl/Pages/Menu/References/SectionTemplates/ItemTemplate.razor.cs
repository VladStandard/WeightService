// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Models;
using WsStorageCore.TableScaleModels.Templates;

namespace BlazorDeviceControl.Pages.Menu.References.SectionTemplates;

public sealed partial class ItemTemplate : RazorComponentItemBase<TemplateModel>
{
    #region Public and private fields, properties, constructor

    private List<TypeModel<string>> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories = BlazorAppSettings.DataSourceDics.GetTemplateCategories();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new List<Action>
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<TemplateModel>(IdentityId);
                if (SqlItemCast.IsNew)
                    SqlItemCast = SqlItemNew<TemplateModel>();
            }
        });
    }

    #endregion
}