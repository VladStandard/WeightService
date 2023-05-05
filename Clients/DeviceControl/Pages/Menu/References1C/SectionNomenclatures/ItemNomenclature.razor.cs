// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusTemplatesFks;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.Templates;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionNomenclatures;

public sealed partial class ItemNomenclature : RazorComponentItemBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor

    private TemplateModel _template;
    private TemplateModel Template { get => _template; set { _template = value; SqlLinkedItems = new() { Template }; } }
    private WsSqlPluTemplateFkModel PluTemplateFk { get; set; }

    public ItemNomenclature() : base()
    {
	    SqlCrudConfigItem.IsGuiShowFilterAdditional = true;
	    SqlCrudConfigItem.IsGuiShowFilterMarked = true;
	    _template = new();
	    PluTemplateFk = new();

        ButtonSettings = new(true, true, true, true, true, true, true);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNullableByUid<WsSqlPluModel>(IdentityUid)
                              ?? SqlItemNew<WsSqlPluModel>();

                ContextManager.AccessManager.AccessList.GetListNotNullable<TemplateModel>(WsSqlCrudConfigUtils
                    .GetCrudConfigComboBox());

                PluTemplateFk = ContextManager.ContextItem.GetItemPluTemplateFkNotNullable(SqlItemCast);
                Template = PluTemplateFk.Template.IsNotNew
                    ? PluTemplateFk.Template
                    : ContextManager.AccessManager.AccessItem.GetItemNewEmpty<TemplateModel>();
            }
        });
    }

    #endregion
}