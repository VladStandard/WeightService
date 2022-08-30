// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.QueriesModels;

namespace BlazorDeviceControl.Razors.Items.Plu;

public partial class ItemPluScale : RazorBase
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureEntity> Nomenclatures { get; set; }
    private List<TemplateEntity> Templates { get; set; }
    private List<ScaleEntity> Scales { get; set; }
    private List<PluEntity> Plus { get; set; }
    private PluScaleEntity ItemCast { get => Item == null ? new() : (PluScaleEntity)Item; set => Item = value; }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusScales);
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
                        ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
                        ItemCast.IsMarked = false;
                        break;
                    default:
                        ItemCast = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluScaleEntity>(IdentityUid);
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
	            //    PluV2Entity pluEntity = AppSettings.DataAccess.PlusCrud.GetItem(
	            //        new FieldListEntity(new Dictionary<string, object,> { { $"Scale.{DbField.IdentityId}", PluItem.Scale.IdentityId } }),
	            //        new FieldOrderEntity { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
	            //    if (pluEntity != null && !pluEntity.EqualsDefault())
	            //    {
	            //        PluItem.Plu = pluEntity.Plu + 1;
	            //    }

	            ButtonSettings = new(false, false, false, false, false, true, true);
            }
        });
    }

    #endregion
}
