//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Xml;

//namespace BlazorDeviceControl.Razors.Items.Plu;

//public partial class ItemPluObsolete : RazorComponentItemBase<PluObsoleteModel>
//{
//    #region Public and private fields, properties, constructor

//    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
//    private List<NomenclatureModel> Nomenclatures { get; set; }
//    private List<ScaleModel> Scales { get; set; }
//    private List<TemplateModel> Templates { get; set; }
//    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

//    public ItemPluObsolete()
//    {
//        Scales = new();
//        Templates = new();
//        Nomenclatures = new();
//    }

//    #endregion

//    #region Public and private methods

//    protected override void OnParametersSet()
//    {
//        RunActionsParametersSet(new()
//        {
//            () =>
//            {
//                SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<PluObsoleteModel>(IdentityId);
//if (SqlItemCast.Identity.IsNew()) SqlItem = SqlItemNew<>();

//	            // Templates.
//                Templates = new() { new() { Title = LocaleCore.Table.FieldNull } };
//                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(nameof(TemplateModel.Title)), 0, false, false);
//				TemplateModel[]? templates = AppSettings.DataAccess.GetItems<TemplateModel>(sqlCrudConfig);
//                if (templates is not null)
//                    Templates.AddRange(templates);

//	            // Nomenclatures.
//                Nomenclatures = new() { new() { Name = LocaleCore.Table.FieldNull } };
//                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(nameof(NomenclatureModel.Name)), 0, false, false);
//				NomenclatureModel[]? nomenclatures = AppSettings.DataAccess.GetItems<NomenclatureModel>(sqlCrudConfig);
//                if (nomenclatures is not null)
//                    Nomenclatures.AddRange(nomenclatures);

//	            // ScaleItems.
//                Scales = new() { new() { Description = LocaleCore.Table.FieldNull } };
//                sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(nameof(SqlTableBase.Description)), 0, false, false);
//				ScaleModel[]? scales = AppSettings.DataAccess.GetItems<ScaleModel>(sqlCrudConfig);
//                if (scales is not null)
//                    Scales.AddRange(scales);

//	            //// Проверка шаблона.
//	            //if ((PluItem.Templates is null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault is not null)
//	            //{
//	            //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
//	            //}
//	            //// Номер PLU.
//	            //if (PluItem.Plu == 0)
//	            //{
//	            //    PluEntity plu = AppSettings.DataAccess.PlusCrud.GetItem(
//	            //        new (new Dictionary<string, object,> { { $"Scale.{DbField.IdentityId}", PluItem.Scale.Identity.Id } }),
//	            //        new FieldOrderModel { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
//	            //    if (plu is not null && !plu.EqualsDefault())
//	            //    {
//	            //        PluItem.Plu = plu.Plu + 1;
//	            //    }

//	            ButtonSettings = new(false, false, false, false, false, true, true);
//            }
//        });
//    }

//    private void OnClickFieldsFill(string name, [CallerMemberName] string memberName = "")
//    {
//        try
//        {
//            switch (name)
//            {
//                case nameof(LocaleCore.DeviceControl.TableActionClear):
//                    SqlItemCast.Nomenclature = new();
//                    SqlItemCast.GoodsName = string.Empty;
//                    SqlItemCast.GoodsFullName = string.Empty;
//                    SqlItemCast.GoodsDescription = string.Empty;
//                    SqlItemCast.GoodsShelfLifeDays = 0;
//                    SqlItemCast.Gtin = string.Empty;
//                    SqlItemCast.Ean13 = string.Empty;
//                    SqlItemCast.Itf14 = string.Empty;
//                    SqlItemCast.GoodsBoxQuantly = 0;
//                    SqlItemCast.GoodsTareWeight = 0;
//                    break;
//                case nameof(LocaleCore.DeviceControl.TableActionFill):
//                    if (string.IsNullOrEmpty(SqlItemCast.GoodsName))
//                        SqlItemCast.GoodsName = ProductHelper.GetXmlName(SqlItemCast.Nomenclature, SqlItemCast.GoodsName);
//                    if (string.IsNullOrEmpty(SqlItemCast.GoodsFullName))
//                        SqlItemCast.GoodsFullName = ProductHelper.GetXmlFullName(SqlItemCast.Nomenclature, SqlItemCast.GoodsFullName);
//                    if (string.IsNullOrEmpty(SqlItemCast.GoodsDescription))
//                        SqlItemCast.GoodsDescription = ProductHelper.GetXmlDescription(SqlItemCast.Nomenclature, SqlItemCast.GoodsDescription);
//                    if (SqlItemCast.GoodsShelfLifeDays == 0)
//                        SqlItemCast.GoodsShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(SqlItemCast.Nomenclature, SqlItemCast.GoodsShelfLifeDays);
//                    if (string.IsNullOrEmpty(SqlItemCast.Gtin))
//                        SqlItemCast.Gtin = ProductHelper.GetXmlGtin(SqlItemCast.Nomenclature, SqlItemCast.Gtin);
//                    if (string.IsNullOrEmpty(SqlItemCast.Ean13))
//                        SqlItemCast.Ean13 = ProductHelper.GetXmlEan13(SqlItemCast.Nomenclature, SqlItemCast.Ean13);
//                    if (string.IsNullOrEmpty(SqlItemCast.Itf14))
//                        SqlItemCast.Itf14 = ProductHelper.GetXmlItf14(SqlItemCast.Nomenclature, SqlItemCast.Itf14);
//                    if (SqlItemCast.GoodsBoxQuantly == 0)
//                        SqlItemCast.GoodsBoxQuantly = ProductHelper.GetXmlBoxQuantly(SqlItemCast.Nomenclature, SqlItemCast.GoodsBoxQuantly);
//                    if (SqlItemCast.GoodsTareWeight == 0)
//                        SqlItemCast.GoodsTareWeight = ProductHelper.CalcGoodsTareWeight(SqlItemCast.Nomenclature);
//                    break;
//                case nameof(ProductHelper.GetXmlName):
//                    SqlItemCast.GoodsName = ProductHelper.GetXmlName(SqlItemCast.Nomenclature, SqlItemCast.GoodsName);
//                    break;
//                case nameof(ProductHelper.GetXmlFullName):
//                    SqlItemCast.GoodsFullName = ProductHelper.GetXmlFullName(SqlItemCast.Nomenclature, SqlItemCast.GoodsFullName);
//                    break;
//                case nameof(ProductHelper.GetXmlDescription):
//                    SqlItemCast.GoodsDescription = ProductHelper.GetXmlDescription(SqlItemCast.Nomenclature, SqlItemCast.GoodsDescription);
//                    break;
//                case nameof(ProductHelper.GetXmlShelfLifeDays):
//                    SqlItemCast.GoodsShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(SqlItemCast.Nomenclature, SqlItemCast.GoodsShelfLifeDays);
//                    break;
//                case nameof(ProductHelper.GetXmlGtin):
//                    SqlItemCast.Gtin = ProductHelper.GetXmlGtin(SqlItemCast.Nomenclature, SqlItemCast.Gtin);
//                    break;
//                case nameof(Barcode.GetGtinWithCheckDigit):
//                    if (SqlItemCast.Gtin.Length > 12)
//                        SqlItemCast.Gtin = Barcode.GetGtinWithCheckDigit(SqlItemCast.Gtin[..13]);
//                    break;
//                case nameof(ProductHelper.GetXmlEan13):
//                    SqlItemCast.Ean13 = ProductHelper.GetXmlEan13(SqlItemCast.Nomenclature, SqlItemCast.Ean13);
//                    break;
//                case nameof(ProductHelper.GetXmlItf14):
//                    SqlItemCast.Itf14 = ProductHelper.GetXmlItf14(SqlItemCast.Nomenclature, SqlItemCast.Itf14);
//                    break;
//                case nameof(ProductHelper.GetXmlBoxQuantly):
//                    SqlItemCast.GoodsBoxQuantly = ProductHelper.GetXmlBoxQuantly(SqlItemCast.Nomenclature, SqlItemCast.GoodsBoxQuantly);
//                    break;
//                case nameof(ProductHelper.CalcGoodsTareWeight):
//                    SqlItemCast.GoodsTareWeight = ProductHelper.CalcGoodsTareWeight(SqlItemCast.Nomenclature);
//                    break;
//            }
//        }
//        catch (Exception ex)
//        {
//            CatchException(ex, GetItemTitle(Item), memberName);
//        }
//    }

//    private string GetWeightFormula()
//    {
//        XmlProductModel xmlProduct = ProductHelper.GetXmlProduct(SqlItemCast.Nomenclature.Xml);
//        // Вес тары = вес коробки + (вес пакета * кол. вложений)
//        return $"{ProductHelper.CalcGoodWeightBox(SqlItemCast.Nomenclature, xmlProduct)} + " +
//               $"({ProductHelper.CalcGoodWeightPack(SqlItemCast.Nomenclature, xmlProduct)} * {ProductHelper.CalcGoodRateUnit(SqlItemCast.Nomenclature, xmlProduct)})";
//    }

//    #endregion
//}
