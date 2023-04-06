// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.Templates;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionNomenclatures;

public sealed partial class ItemNomenclature : RazorComponentItemBase<PluModel>
{
    #region Public and private fields, properties, constructor

    private TemplateModel _template;
    private TemplateModel Template { get => _template; set { _template = value; SqlLinkedItems = new() { Template }; } }
    private PluTemplateFkModel PluTemplateFk { get; set; }

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
                SqlItemCast = DataContext.GetItemNullableByUid<PluModel>(IdentityUid) 
                              ?? SqlItemNew <PluModel>();
                
                DataContext.GetListNotNullable<TemplateModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());

                PluTemplateFk = DataAccess.GetItemPluTemplateFkNotNullable(SqlItemCast);
                Template = PluTemplateFk.Template.IsNotNew ? PluTemplateFk.Template : DataAccess.GetItemNewEmpty<TemplateModel>();
            }
        });
    }

    #endregion
}
