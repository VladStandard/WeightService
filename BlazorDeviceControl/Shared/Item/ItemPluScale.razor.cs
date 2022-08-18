// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.DataModels;
using DataCore.Sql.TableScaleModels;
using System.Runtime.CompilerServices;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemPluScale : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureEntity> Nomenclatures { get; set; }
    private List<TemplateEntity> Templates { get; set; }
	private List<ScaleEntity> Scales { get; set; }
	private List<PluEntity> Plus { get; set; }
    private PluScaleEntity ItemCast { get => Item == null ? new() : (PluScaleEntity)Item; set => Item = value; }
    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

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
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<PluScaleEntity>(
                            new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
                        break;
                }

	            // Templates.
	            List<TemplateEntity>? templates = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.Title))
                    ?.ToList();
                if (templates is not null)
                {
	                Templates = new() { new(0, false) { Title = LocaleCore.Table.FieldNull } };
	                Templates.AddRange(templates);
                }

	            // Nomenclatures.
	            List<NomenclatureEntity>? nomenclatures = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(
                        new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.Name))
                    ?.ToList();
                if (nomenclatures is not null)
                {
	                Nomenclatures = new() { new(0, false) { Name = LocaleCore.Table.FieldNull } };
	                Nomenclatures.AddRange(nomenclatures);
                }

	            // Scales.
	            List<ScaleEntity>? scales = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Name))
                    ?.ToList();
                if (scales is not null)
                {
	                Scales = new() { new(0, false) { Description = LocaleCore.Table.FieldNull } };
	                Scales.AddRange(scales);
                }

	            // Plus.
	            List<PluEntity>? plus = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Name))
                    ?.ToList();
                if (plus is not null)
                {
	                Plus = new() { new(0, false) { Description = LocaleCore.Table.FieldNull } };
	                Plus.AddRange(plus);
                }

	            //// Проверка шаблона.
	            //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
	            //{
	            //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
	            //}
	            //// Номер PLU.
	            //if (PluItem.Plu == 0)
	            //{
	            //    PluV2Entity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
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

    private void OnClickFieldsFill(string name,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        try
        {
            switch (name)
            {
                case nameof(LocaleData.DeviceControl.TableActionClear):
                    ItemCast.Plu.Nomenclature = new();
                    ItemCast.Plu.Name = string.Empty;
                    ItemCast.Plu.FullName = string.Empty;
                    ItemCast.Plu.Description = string.Empty;
                    ItemCast.Plu.ShelfLifeDays = 0;
                    ItemCast.Plu.Gtin = string.Empty;
                    ItemCast.Plu.Ean13 = string.Empty;
                    ItemCast.Plu.Itf14 = string.Empty;
                    ItemCast.Plu.BoxQuantly = 0;
                    ItemCast.Plu.TareWeight = 0;
                    break;
                case nameof(LocaleData.DeviceControl.TableActionFill):
                    if (string.IsNullOrEmpty(ItemCast.Plu.Name))
                        ItemCast.Plu.Name = ProductHelper.GetXmlName(ItemCast.Plu.Nomenclature, ItemCast.Plu.Name);
                    if (string.IsNullOrEmpty(ItemCast.Plu.FullName))
                        ItemCast.Plu.FullName = ProductHelper.GetXmlFullName(ItemCast.Plu.Nomenclature, ItemCast.Plu.FullName);
                    if (string.IsNullOrEmpty(ItemCast.Plu.Description))
                        ItemCast.Plu.Description = ProductHelper.GetXmlDescription(ItemCast.Plu.Nomenclature, ItemCast.Plu.Description);
                    if (ItemCast.Plu.ShelfLifeDays == 0)
                        ItemCast.Plu.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Plu.Nomenclature, ItemCast.Plu.ShelfLifeDays);
                    if (string.IsNullOrEmpty(ItemCast.Plu.Gtin))
                        ItemCast.Plu.Gtin = ProductHelper.GetXmlGtin(ItemCast.Plu.Nomenclature, ItemCast.Plu.Gtin);
                    if (string.IsNullOrEmpty(ItemCast.Plu.Ean13))
                        ItemCast.Plu.Ean13 = ProductHelper.GetXmlEan13(ItemCast.Plu.Nomenclature, ItemCast.Plu.Ean13);
                    if (string.IsNullOrEmpty(ItemCast.Plu.Itf14))
                        ItemCast.Plu.Itf14 = ProductHelper.GetXmlItf14(ItemCast.Plu.Nomenclature, ItemCast.Plu.Itf14);
                    if (ItemCast.Plu.BoxQuantly == 0)
                        ItemCast.Plu.BoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Plu.Nomenclature, ItemCast.Plu.BoxQuantly);
                    if (ItemCast.Plu.TareWeight == 0)
                        ItemCast.Plu.TareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Plu.Nomenclature);
                    break;
                case nameof(ProductHelper.GetXmlName):
                    ItemCast.Plu.Name = ProductHelper.GetXmlName(ItemCast.Plu.Nomenclature, ItemCast.Plu.Name);
                    break;
                case nameof(ProductHelper.GetXmlFullName):
                    ItemCast.Plu.FullName = ProductHelper.GetXmlFullName(ItemCast.Plu.Nomenclature, ItemCast.Plu.FullName);
                    break;
                case nameof(ProductHelper.GetXmlDescription):
                    ItemCast.Plu.Description = ProductHelper.GetXmlDescription(ItemCast.Plu.Nomenclature, ItemCast.Plu.Description);
                    break;
                case nameof(ProductHelper.GetXmlShelfLifeDays):
                    ItemCast.Plu.ShelfLifeDays = ProductHelper.GetXmlShelfLifeDays(ItemCast.Plu.Nomenclature, ItemCast.Plu.ShelfLifeDays);
                    break;
                case nameof(ProductHelper.GetXmlGtin):
                    ItemCast.Plu.Gtin = ProductHelper.GetXmlGtin(ItemCast.Plu.Nomenclature, ItemCast.Plu.Gtin);
                    break;
                case nameof(Barcode.GetGtinWithCheckDigit):
                    if (ItemCast.Plu.Gtin.Length > 12)
                        ItemCast.Plu.Gtin = Barcode.GetGtinWithCheckDigit(ItemCast.Plu.Gtin[..13]);
                    break;
                case nameof(ProductHelper.GetXmlEan13):
                    ItemCast.Plu.Ean13 = ProductHelper.GetXmlEan13(ItemCast.Plu.Nomenclature, ItemCast.Plu.Ean13);
                    break;
                case nameof(ProductHelper.GetXmlItf14):
                    ItemCast.Plu.Itf14 = ProductHelper.GetXmlItf14(ItemCast.Plu.Nomenclature, ItemCast.Plu.Itf14);
                    break;
                case nameof(ProductHelper.GetXmlBoxQuantly):
                    ItemCast.Plu.BoxQuantly = ProductHelper.GetXmlBoxQuantly(ItemCast.Plu.Nomenclature, ItemCast.Plu.BoxQuantly);
                    break;
                case nameof(ProductHelper.CalcGoodsTareWeight):
                    ItemCast.Plu.TareWeight = ProductHelper.CalcGoodsTareWeight(ItemCast.Plu.Nomenclature);
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
        XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(ItemCast.Plu.Nomenclature.Xml);
        // Вес тары = вес коробки + (вес пакета * кол. вложений)
        return $"{ProductHelper.CalcGoodWeightBox(ItemCast.Plu.Nomenclature, xmlProduct)} + " +
               $"({ProductHelper.CalcGoodWeightPack(ItemCast.Plu.Nomenclature, xmlProduct)} * " +
               $"{ProductHelper.CalcGoodRateUnit(ItemCast.Plu.Nomenclature, xmlProduct)})";
    }

    #endregion
}
