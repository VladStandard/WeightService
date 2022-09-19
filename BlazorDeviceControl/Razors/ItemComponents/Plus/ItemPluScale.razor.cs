// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPluScale : RazorComponentItemBase<PluScaleModel>
{
    #region Public and private fields, properties, constructor

    private List<NomenclatureModel> Nomenclatures { get; set; }
    private List<TemplateModel> Templates { get; set; }
    private List<ScaleModel> Scales { get; set; }
    private List<PluModel> Plus { get; set; }

    public ItemPluScale()
    {
        Templates = new();
        Nomenclatures = new();
        Scales = new();
        Plus = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                switch (TableAction)
                {
                    case SqlTableActionEnum.New:
                        SqlItemCast = new();
                        SqlItemCast.SetDtNow();
						SqlItemCast.IsMarked = false;
                        break;
                    default:
                        SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                        break;
                }
	            Templates = AppSettings.DataAccess.GetListTemplates(false, false, true);
	            Nomenclatures = AppSettings.DataAccess.GetListNomenclatures(false, false, true);
	            Scales = AppSettings.DataAccess.GetListScales(false, false, true);
                Plus = AppSettings.DataAccess.GetListPlus(false, false, true);

	            //// Проверка шаблона.
	            //if ((PluItem.Templates is null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
	            //{
	            //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
	            //}
	            //// Номер PLU.
	            //if (PluItem.Plu == 0)
	            //{
	            //    PluModel plu = AppSettings.DataAccess.PlusCrud.GetItem(
	            //        new (new Dictionary<string, object,> { { $"Scale.{DbField.IdentityId}", PluItem.Scale.Identity.Id } }),
	            //        new FieldOrderModel { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
	            //    if (plu != null && !plu.EqualsDefault())
	            //    {
	            //        PluItem.Plu = plu.Plu + 1;
	            //    }

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
