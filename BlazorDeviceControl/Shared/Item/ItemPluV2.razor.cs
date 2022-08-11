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

public partial class ItemPluV2
{
    #region Public and private fields, properties, constructor

    private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
    private List<NomenclatureEntity> Nomenclatures { get; set; }
    private List<TemplateEntity> Templates { get; set; }
    private PluV2Entity ItemCast { get => Item == null ? new() : (PluV2Entity)Item; set => Item = value; }
    private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusV2);
        ItemCast = new();
        Templates = new();
        Nomenclatures = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
                typeof(ItemPluV2).GetConstructor(Type.EmptyTypes)?.Invoke(new object[] { });
            },
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
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<PluV2Entity>(
                            new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
                        break;
                }

	            // Templates.
	            List<TemplateEntity>? templates = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.Title, DbOrderDirection.Asc))
                    ?.ToList();
                if (templates is not null)
                {
                    Templates.Add(new(0) { Title = LocaleCore.Table.FieldNull });
                    Templates.AddRange(templates);
                }

	            // Nomenclatures.
	            List<NomenclatureEntity>? nomenclatures = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(
                        new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.Name, DbOrderDirection.Asc))
                    ?.ToList();
                if (nomenclatures is not null)
                {
                    Nomenclatures.Add(new(0) { Name = LocaleCore.Table.FieldNull });
                    Nomenclatures.AddRange(nomenclatures);
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
        XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(ItemCast.Nomenclature.Xml);
        // Вес тары = вес коробки + (вес пакета * кол. вложений)
        return $"{ProductHelper.CalcGoodWeightBox(ItemCast.Nomenclature, xmlProduct)} + " +
               $"({ProductHelper.CalcGoodWeightPack(ItemCast.Nomenclature, xmlProduct)} * {ProductHelper.CalcGoodRateUnit(ItemCast.Nomenclature, xmlProduct)})";
    }

    #endregion
}
