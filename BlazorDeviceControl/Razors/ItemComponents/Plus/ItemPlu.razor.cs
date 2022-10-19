// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.Fields;
using DataCore.Sql.Xml;

namespace BlazorDeviceControl.Razors.ItemComponents.Plus;

public partial class ItemPlu : RazorComponentItemBase<PluModel>
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureModel> Nomenclatures { get; set; }
    private List<TemplateModel> Templates { get; set; }
    //private List<ScaleModel> Scales { get; set; }
    //private List<PluModel> Plus { get; set; }
    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

    public ItemPlu()
    {
        RazorComponentConfig.IsShowFilterAdditional = true;
        RazorComponentConfig.IsShowFilterMarked = true;
        Templates = new();
        Nomenclatures = new();
        //Scales = new();
        //Plus = new();
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<PluModel>(IdentityUid);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<PluModel>();

	            // Templates.
	            Templates = new() { AppSettings.DataAccess.GetNewTemplate() };
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(0, false, false);
                TemplateModel[]? templates = AppSettings.DataAccess.GetItems<TemplateModel>(sqlCrudConfig);
                if (templates is not null)
                    Templates.AddRange(templates);

	            // Nomenclatures.
	            Nomenclatures = new() { AppSettings.DataAccess.GetNewNomenclature() };
                sqlCrudConfig = SqlUtils.GetCrudConfig(
                    new SqlFieldOrderModel(nameof(NomenclatureModel.Name), SqlFieldOrderEnum.Asc), 0, false, false);
                NomenclatureModel[]? nomenclatures = AppSettings.DataAccess.GetItems<NomenclatureModel>(sqlCrudConfig);
                if (nomenclatures is not null)
                    Nomenclatures.AddRange(nomenclatures);

	            // Scales.
	            //Scales = new() { AppSettings.DataAccess.GetNewScale() };
             //   sqlCrudConfig = SqlUtils.GetCrudConfig(
             //       new SqlFieldOrderModel(nameof(ScaleModel.Description), SqlFieldOrderEnum.Asc), 0, false, false);
             //   ScaleModel[]? scales = AppSettings.DataAccess.GetItems<ScaleModel>(sqlCrudConfig);
             //   if (scales is not null)
             //       Scales.AddRange(scales);

	            // Plus.
	            //Plus = new() { AppSettings.DataAccess.GetNewPlu() };
             //   sqlCrudConfig = SqlUtils.GetCrudConfig(
             //       new SqlFieldOrderModel(nameof(PluModel.Name), SqlFieldOrderEnum.Asc), 0, false, false);
             //   PluModel[]? plus = AppSettings.DataAccess.GetItems<PluModel>(sqlCrudConfig);
             //   if (plus is not null)
             //       Plus.AddRange(plus);

	            //// Проверка шаблона.
	            //if ((PluItem.Templates is null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault is not null)
	            //{
	            //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
	            //}
	            //// Номер PLU.
	            //if (PluItem.Plu == 0)
	            //{
	            //    PluModel plu = AppSettings.DataAccess.PlusCrud.GetItem(
	            //        new (new Dictionary<string, object,> { { $"Scale.{DbField.IdentityId}", PluItem.Scale.Identity.Id } }),
	            //        new FieldOrderModel { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
	            //    if (plu is not null && !plu.EqualsDefault())
	            //    {
	            //        PluItem.Plu = plu.Plu + 1;
	            //    }

	            ButtonSettings = new(true, true, true, true, true, true, false);
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
                    SqlItemCast.Nomenclature = new();
                    SqlItemCast.Name = string.Empty;
                    SqlItemCast.FullName = string.Empty;
                    SqlItemCast.Description = string.Empty;
                    SqlItemCast.ShelfLifeDays = 0;
                    SqlItemCast.Gtin = string.Empty;
                    SqlItemCast.Ean13 = string.Empty;
                    SqlItemCast.Itf14 = string.Empty;
                    SqlItemCast.BoxQuantly = 0;
                    //SqlItemCast.TareWeight = 0;
                    break;
                case nameof(LocaleCore.DeviceControl.TableActionFill):
                    if (string.IsNullOrEmpty(SqlItemCast.Name))
                        SqlItemCast.Name = ProductHelper.GetXmlName(SqlItemCast.Nomenclature, SqlItemCast.Name);
                    if (string.IsNullOrEmpty(SqlItemCast.FullName))
                        SqlItemCast.FullName = ProductHelper.GetXmlFullName(SqlItemCast.Nomenclature, SqlItemCast.FullName);
                    if (string.IsNullOrEmpty(SqlItemCast.Description))
                        SqlItemCast.Description = ProductHelper.GetXmlDescription(SqlItemCast.Nomenclature, SqlItemCast.Description);
                    if (SqlItemCast.ShelfLifeDays == 0)
                        SqlItemCast.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(SqlItemCast.Nomenclature, SqlItemCast.ShelfLifeDays);
                    if (string.IsNullOrEmpty(SqlItemCast.Gtin))
                        SqlItemCast.Gtin = ProductHelper.GetXmlGtin(SqlItemCast.Nomenclature, SqlItemCast.Gtin);
                    if (string.IsNullOrEmpty(SqlItemCast.Ean13))
                        SqlItemCast.Ean13 = ProductHelper.GetXmlEan13(SqlItemCast.Nomenclature, SqlItemCast.Ean13);
                    if (string.IsNullOrEmpty(SqlItemCast.Itf14))
                        SqlItemCast.Itf14 = ProductHelper.GetXmlItf14(SqlItemCast.Nomenclature, SqlItemCast.Itf14);
                    if (SqlItemCast.BoxQuantly == 0)
                        SqlItemCast.BoxQuantly = ProductHelper.GetXmlBoxQuantly(SqlItemCast.Nomenclature, SqlItemCast.BoxQuantly);
                    //if (SqlItemCast.TareWeight == 0)
                    //    SqlItemCast.TareWeight = ProductHelper.CalcGoodsTareWeight(SqlItemCast.Nomenclature);
                    break;
                case nameof(ProductHelper.GetXmlName):
                    SqlItemCast.Name = ProductHelper.GetXmlName(SqlItemCast.Nomenclature, SqlItemCast.Name);
                    break;
                case nameof(ProductHelper.GetXmlFullName):
                    SqlItemCast.FullName = ProductHelper.GetXmlFullName(SqlItemCast.Nomenclature, SqlItemCast.FullName);
                    break;
                case nameof(ProductHelper.GetXmlDescription):
                    SqlItemCast.Description = ProductHelper.GetXmlDescription(SqlItemCast.Nomenclature, SqlItemCast.Description);
                    break;
                case nameof(ProductHelper.GetXmlShelfLifeDays):
                    SqlItemCast.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(SqlItemCast.Nomenclature, SqlItemCast.ShelfLifeDays);
                    break;
                case nameof(ProductHelper.GetXmlGtin):
                    SqlItemCast.Gtin = ProductHelper.GetXmlGtin(SqlItemCast.Nomenclature, SqlItemCast.Gtin);
                    break;
                case nameof(Barcode.GetGtinWithCheckDigit):
                    if (SqlItemCast.Gtin.Length > 12)
                        SqlItemCast.Gtin = Barcode.GetGtinWithCheckDigit(SqlItemCast.Gtin[..13]);
                    break;
                case nameof(ProductHelper.GetXmlEan13):
                    SqlItemCast.Ean13 = ProductHelper.GetXmlEan13(SqlItemCast.Nomenclature, SqlItemCast.Ean13);
                    break;
                case nameof(ProductHelper.GetXmlItf14):
                    SqlItemCast.Itf14 = ProductHelper.GetXmlItf14(SqlItemCast.Nomenclature, SqlItemCast.Itf14);
                    break;
                case nameof(ProductHelper.GetXmlBoxQuantly):
                    SqlItemCast.BoxQuantly = ProductHelper.GetXmlBoxQuantly(SqlItemCast.Nomenclature, SqlItemCast.BoxQuantly);
                    break;
                case nameof(ProductHelper.CalcGoodsTareWeight):
                    //SqlItemCast.TareWeight = ProductHelper.CalcGoodsTareWeight(SqlItemCast.Nomenclature);
                    break;
            }
        }
        catch (Exception ex)
        {
            CatchException(ex, GetItemTitle(SqlItem), memberName);
        }
    }

    #endregion
}
