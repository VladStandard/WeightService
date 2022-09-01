// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.Items.Plu;

public partial class ItemPluScale : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureModel> Nomenclatures { get; set; }
    private List<TemplateModel> Templates { get; set; }
    private List<ScaleModel> Scales { get; set; }
    private List<PluModel> Plus { get; set; }
    private PluScaleModel ItemCast { get => Item == null ? new() : (PluScaleModel)Item; set => Item = value; }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleModel(ProjectsEnums.TableScale.PlusScales);
        ItemCast = new();
        Templates = new();
        Nomenclatures = new();
        Scales = new();
        Plus = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
                switch (TableAction)
                {
                    case DbTableAction.New:
                        ItemCast = new();
                        ItemCast.SetDt();
						ItemCast.IsMarked = false;
                        break;
                    default:
                        ItemCast = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluScaleModel>(IdentityUid);
                        break;
                }
	            Templates = AppSettings.DataAccess.Crud.GetListTemplates(false, false, true);
	            Nomenclatures = AppSettings.DataAccess.Crud.GetListNomenclatures(false, false, true);
	            Scales = AppSettings.DataAccess.Crud.GetListScales(false, false, true);
                Plus = AppSettings.DataAccess.Crud.GetListPlus(false, false, true);

	            //// Проверка шаблона.
	            //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
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
