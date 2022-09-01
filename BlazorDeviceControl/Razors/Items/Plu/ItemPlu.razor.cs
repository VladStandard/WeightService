// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.Items.Plu;

public partial class ItemPlu : RazorBase
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureModel> Nomenclatures { get; set; }
    private List<TemplateModel> Templates { get; set; }
    private List<ScaleModel> Scales { get; set; }
    private List<PluModel> Plus { get; set; }
    private PluModel ItemCast { get => Item == null ? new() : (PluModel)Item; set => Item = value; }
    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleModel(ProjectsEnums.TableScale.Plus);
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
	                    //Guid uid = ItemFilter.Identity.Uid;
                        ItemCast = AppSettings.DataAccess.Crud.GetItemByUidNotNull<PluModel>(IdentityUid);
                        break;
                }

	            // Templates.
	            Templates = new() { new() { Title = LocaleCore.Table.FieldNull } };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Title), 0, false, false);
                TemplateModel[]? templates = AppSettings.DataAccess.Crud.GetItems<TemplateModel>(sqlCrudConfig);
                if (templates is not null)
                    Templates.AddRange(templates);

	            // Nomenclatures.
	            Nomenclatures = new() { new() { Name = LocaleCore.Table.FieldNull } };
                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, false, false);
                NomenclatureModel[]? nomenclatures = AppSettings.DataAccess.Crud.GetItems<NomenclatureModel>(sqlCrudConfig);
                if (nomenclatures is not null)
                    Nomenclatures.AddRange(nomenclatures);

	            // Scales.
	            Scales = new() { new() { Description = LocaleCore.Table.FieldNull } };
                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, false, false);
                ScaleModel[]? scales = AppSettings.DataAccess.Crud.GetItems<ScaleModel>(sqlCrudConfig);
                if (scales is not null)
                    Scales.AddRange(scales);

	            // Plus.
	            Plus = new() { new() { Description = LocaleCore.Table.FieldNull } };
                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Name), 0, false, false);
                PluModel[]? plus = AppSettings.DataAccess.Crud.GetItems<PluModel>(sqlCrudConfig);
                if (plus is not null)
                    Plus.AddRange(plus);

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

    private void OnClickFieldsFill(string name,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            switch (name)
            {
                case nameof(LocaleData.DeviceControl.TableActionClear):
                    ItemCast.Nomenclature = new();
                    ItemCast.Name = string.Empty;
                    ItemCast.FullName = string.Empty;
                    ItemCast.Description = string.Empty;
                    ItemCast.ShelfLifeDays = 0;
                    ItemCast.Gtin = string.Empty;
                    ItemCast.Ean13 = string.Empty;
                    ItemCast.Itf14 = string.Empty;
                    ItemCast.BoxQuantly = 0;
                    ItemCast.TareWeight = 0;
                    break;
                case nameof(LocaleData.DeviceControl.TableActionFill):
                    if (string.IsNullOrEmpty(ItemCast.Name))
                        ItemCast.Name = ProductHelper.GetXmlName(ItemCast.Nomenclature, ItemCast.Name);
                    if (string.IsNullOrEmpty(ItemCast.FullName))
                        ItemCast.FullName = ProductHelper.GetXmlFullName(ItemCast.Nomenclature, ItemCast.FullName);
                    if (string.IsNullOrEmpty(ItemCast.Description))
                        ItemCast.Description = ProductHelper.GetXmlDescription(ItemCast.Nomenclature, ItemCast.Description);
                    if (ItemCast.ShelfLifeDays == 0)
                        ItemCast.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Nomenclature, ItemCast.ShelfLifeDays);
                    if (string.IsNullOrEmpty(ItemCast.Gtin))
                        ItemCast.Gtin = ProductHelper.GetXmlGtin(ItemCast.Nomenclature, ItemCast.Gtin);
                    if (string.IsNullOrEmpty(ItemCast.Ean13))
                        ItemCast.Ean13 = ProductHelper.GetXmlEan13(ItemCast.Nomenclature, ItemCast.Ean13);
                    if (string.IsNullOrEmpty(ItemCast.Itf14))
                        ItemCast.Itf14 = ProductHelper.GetXmlItf14(ItemCast.Nomenclature, ItemCast.Itf14);
                    if (ItemCast.BoxQuantly == 0)
                        ItemCast.BoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Nomenclature, ItemCast.BoxQuantly);
                    if (ItemCast.TareWeight == 0)
                        ItemCast.TareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Nomenclature);
                    break;
                case nameof(ProductHelper.GetXmlName):
                    ItemCast.Name = ProductHelper.GetXmlName(ItemCast.Nomenclature, ItemCast.Name);
                    break;
                case nameof(ProductHelper.GetXmlFullName):
                    ItemCast.FullName = ProductHelper.GetXmlFullName(ItemCast.Nomenclature, ItemCast.FullName);
                    break;
                case nameof(ProductHelper.GetXmlDescription):
                    ItemCast.Description = ProductHelper.GetXmlDescription(ItemCast.Nomenclature, ItemCast.Description);
                    break;
                case nameof(ProductHelper.GetXmlShelfLifeDays):
                    ItemCast.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Nomenclature, ItemCast.ShelfLifeDays);
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
                    ItemCast.BoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Nomenclature, ItemCast.BoxQuantly);
                    break;
                case nameof(ProductHelper.CalcGoodsTareWeight):
                    ItemCast.TareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Nomenclature);
                    break;
            }
        }
        catch (Exception ex)
        {
            RunTasksCatch(ex, Table.Name, memberName, filePath, lineNumber, memberName);
        }
    }

    private string GetWeightFormula()
    {
        XmlProductModel xmlProduct = ProductHelper.GetXmlProduct(ItemCast.Nomenclature.Xml);
        // Вес тары = вес коробки + (вес пакета * кол. вложений)
        return $"{ProductHelper.CalcGoodWeightBox(ItemCast.Nomenclature, xmlProduct)} + " +
               $"({ProductHelper.CalcGoodWeightPack(ItemCast.Nomenclature, xmlProduct)} * " +
               $"{ProductHelper.CalcGoodRateUnit(ItemCast.Nomenclature, xmlProduct)})";
    }

    #endregion
}
