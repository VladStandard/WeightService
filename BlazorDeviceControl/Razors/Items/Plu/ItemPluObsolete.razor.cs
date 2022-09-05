// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.Items.Plu;

public partial class ItemPluObsolete : RazorPageModel
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureModel> Nomenclatures { get; set; }
    private List<ScaleModel> Scales { get; set; }
    private List<TemplateModel> Templates { get; set; }
    private PluObsoleteModel ItemCast { get => Item == null ? new() : (PluObsoleteModel)Item; set => Item = value; }
    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleModel(ProjectsEnums.TableScale.PlusObsolete);
        ItemCast = new();
        Scales = new();
        Templates = new();
        Nomenclatures = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsSilent(new()
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
                        ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PluObsoleteModel>(IdentityId);
                        break;
                }

	            // Templates.
                Templates = new() { new() { Title = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Title), 0, false, false);
				TemplateModel[]? templates = AppSettings.DataAccess.GetItems<TemplateModel>(sqlCrudConfig);
                if (templates is not null)
                    Templates.AddRange(templates);

	            // Nomenclatures.
                Nomenclatures = new() { new() { Name = LocaleCore.Table.FieldNull } };
                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, false, false);
				NomenclatureModel[]? nomenclatures = AppSettings.DataAccess.GetItems<NomenclatureModel>(sqlCrudConfig);
                if (nomenclatures is not null)
                    Nomenclatures.AddRange(nomenclatures);

	            // ScaleItems.
                Scales = new() { new() { Description = LocaleCore.Table.FieldNull } };
                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
				ScaleModel[]? scales = AppSettings.DataAccess.GetItems<ScaleModel>(sqlCrudConfig);
                if (scales is not null)
                    Scales.AddRange(scales);

	            //// Проверка шаблона.
	            //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
	            //{
	            //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
	            //}
	            //// Номер PLU.
	            //if (PluItem.Plu == 0)
	            //{
	            //    PluEntity plu = AppSettings.DataAccess.PlusCrud.GetItem(
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

    private void OnClickFieldsFill(string name, [CallerMemberName] string memberName = "")
    {
        try
        {
            switch (name)
            {
                case nameof(LocaleCore.DeviceControl.TableActionClear):
                    ItemCast.Nomenclature = new();
                    ItemCast.GoodsName = string.Empty;
                    ItemCast.GoodsFullName = string.Empty;
                    ItemCast.GoodsDescription = string.Empty;
                    ItemCast.GoodsShelfLifeDays = 0;
                    ItemCast.Gtin = string.Empty;
                    ItemCast.Ean13 = string.Empty;
                    ItemCast.Itf14 = string.Empty;
                    ItemCast.GoodsBoxQuantly = 0;
                    ItemCast.GoodsTareWeight = 0;
                    break;
                case nameof(LocaleCore.DeviceControl.TableActionFill):
                    if (string.IsNullOrEmpty(ItemCast.GoodsName))
                        ItemCast.GoodsName = ProductHelper.GetXmlName(ItemCast.Nomenclature, ItemCast.GoodsName);
                    if (string.IsNullOrEmpty(ItemCast.GoodsFullName))
                        ItemCast.GoodsFullName = ProductHelper.GetXmlFullName(ItemCast.Nomenclature, ItemCast.GoodsFullName);
                    if (string.IsNullOrEmpty(ItemCast.GoodsDescription))
                        ItemCast.GoodsDescription = ProductHelper.GetXmlDescription(ItemCast.Nomenclature, ItemCast.GoodsDescription);
                    if (ItemCast.GoodsShelfLifeDays == 0)
                        ItemCast.GoodsShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Nomenclature, ItemCast.GoodsShelfLifeDays);
                    if (string.IsNullOrEmpty(ItemCast.Gtin))
                        ItemCast.Gtin = ProductHelper.GetXmlGtin(ItemCast.Nomenclature, ItemCast.Gtin);
                    if (string.IsNullOrEmpty(ItemCast.Ean13))
                        ItemCast.Ean13 = ProductHelper.GetXmlEan13(ItemCast.Nomenclature, ItemCast.Ean13);
                    if (string.IsNullOrEmpty(ItemCast.Itf14))
                        ItemCast.Itf14 = ProductHelper.GetXmlItf14(ItemCast.Nomenclature, ItemCast.Itf14);
                    if (ItemCast.GoodsBoxQuantly == 0)
                        ItemCast.GoodsBoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Nomenclature, ItemCast.GoodsBoxQuantly);
                    if (ItemCast.GoodsTareWeight == 0)
                        ItemCast.GoodsTareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Nomenclature);
                    break;
                case nameof(ProductHelper.GetXmlName):
                    ItemCast.GoodsName = ProductHelper.GetXmlName(ItemCast.Nomenclature, ItemCast.GoodsName);
                    break;
                case nameof(ProductHelper.GetXmlFullName):
                    ItemCast.GoodsFullName = ProductHelper.GetXmlFullName(ItemCast.Nomenclature, ItemCast.GoodsFullName);
                    break;
                case nameof(ProductHelper.GetXmlDescription):
                    ItemCast.GoodsDescription = ProductHelper.GetXmlDescription(ItemCast.Nomenclature, ItemCast.GoodsDescription);
                    break;
                case nameof(ProductHelper.GetXmlShelfLifeDays):
                    ItemCast.GoodsShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Nomenclature, ItemCast.GoodsShelfLifeDays);
                    break;
                case nameof(ProductHelper.GetXmlGtin):
                    ItemCast.Gtin = ProductHelper.GetXmlGtin(ItemCast.Nomenclature, ItemCast.Gtin);
                    break;
                case nameof(Barcode.GetGtinWithCheckDigit):
                    if (ItemCast.Gtin.Length > 12)
                        ItemCast.Gtin = Barcode.GetGtinWithCheckDigit(ItemCast.Gtin[..13]);
                    break;
                case nameof(ProductHelper.GetXmlEan13):
                    ItemCast.Ean13 = ProductHelper.GetXmlEan13(ItemCast.Nomenclature, ItemCast.Ean13);
                    break;
                case nameof(ProductHelper.GetXmlItf14):
                    ItemCast.Itf14 = ProductHelper.GetXmlItf14(ItemCast.Nomenclature, ItemCast.Itf14);
                    break;
                case nameof(ProductHelper.GetXmlBoxQuantly):
                    ItemCast.GoodsBoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Nomenclature, ItemCast.GoodsBoxQuantly);
                    break;
                case nameof(ProductHelper.CalcGoodsTareWeight):
                    ItemCast.GoodsTareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Nomenclature);
                    break;
            }
        }
        catch (Exception ex)
        {
            CatchException(ex, Table.Name, memberName);
        }
    }

    private string GetWeightFormula()
    {
        XmlProductModel xmlProduct = ProductHelper.GetXmlProduct(ItemCast.Nomenclature.Xml);
        // Вес тары = вес коробки + (вес пакета * кол. вложений)
        return $"{ProductHelper.CalcGoodWeightBox(ItemCast.Nomenclature, xmlProduct)} + " +
               $"({ProductHelper.CalcGoodWeightPack(ItemCast.Nomenclature, xmlProduct)} * {ProductHelper.CalcGoodRateUnit(ItemCast.Nomenclature, xmlProduct)})";
    }

    #endregion
}
