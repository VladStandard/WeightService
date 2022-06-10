// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.DataModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemPlu
    {
        #region Public and private fields and properties

        private BarcodeHelper Barcode { get; } = BarcodeHelper.Instance;
        private List<NomenclatureEntity> Nomenclatures { get; set; }
        private List<NomenclatureEntity>? NomenclatureItems { get; set; }
        private List<ScaleEntity> ScaleItems { get; set; }
        private List<TemplateEntity> Templates { get; set; }
        private PluEntity ItemCast { get => Item == null ? new() : (PluEntity)Item; set => Item = value; }
        private XmlProductHelper ProductHelper { get; } = XmlProductHelper.Instance;

        #endregion

        #region Constructor and destructor

        public ItemPlu() : base()
        {
            ScaleItems = new();
            Templates = new();
            Nomenclatures = new();
            Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
            ItemCast = new();
            ButtonSettings = new();
            
            ScaleItems = new();
            Templates = new();
            Nomenclatures = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
                            ItemCast.IsMarked = false;
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(
                                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, IdentityId } }), null);
                            break;
                    }

                    // Templates.
                    List<TemplateEntity>? templates = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Title, DbOrderDirection.Asc))
                        ?.ToList();
                    if (templates is { } templates2)
                    {
                        Templates.Add(new TemplateEntity(0) { Title = LocaleCore.Table.FieldNull });
                        Templates.AddRange(templates2);
                    }
                    // Nomenclatures.
                    List<NomenclatureEntity>? nomenclatures = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc))
                        ?.ToList();
                    if (nomenclatures is not null)
                    {
                        Nomenclatures.Add(new NomenclatureEntity(0) { Name = LocaleCore.Table.FieldNull });
                        Nomenclatures.AddRange(nomenclatures);
                    }

                    // ScaleItems.
                    List<ScaleEntity>? scales = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        new FieldOrderEntity(DbField.Description, DbOrderDirection.Asc))
                        ?.ToList();
                    if (scales is not null)
                    {
                        ScaleItems.Add(new ScaleEntity(0) { Description = LocaleCore.Table.FieldNull });
                        ScaleItems.AddRange(scales);
                    }

                    //// Проверка шаблона.
                    //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
                    //{
                    //    PluItem.Templates = PluItem.Scale.TemplateDefault.CloneCast();
                    //}
                    //// Номер PLU.
                    //if (PluItem.Plu == 0)
                    //{
                    //    PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
                    //        new FieldListEntity(new Dictionary<string, object,> { { $"Scale.{DbField.IdentityId}", PluItem.Scale.IdentityId } }),
                    //        new FieldOrderEntity { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
                    //    if (pluEntity != null && !pluEntity.EqualsDefault())
                    //    {
                    //        PluItem.Plu = pluEntity.Plu + 1;
                    //    }
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnClickFieldsFill(string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            if (ItemCast?.Nomenclature == null)
                return;
            try
            {
                switch (name)
                {
                    case nameof(LocaleData.DeviceControl.TableActionClear):
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
                    case nameof(LocaleData.DeviceControl.TableActionFill):
                        if (string.IsNullOrEmpty(ItemCast.GoodsName))
                            ItemCast.GoodsName = ItemCast.XmlGoodsName;
                        if (string.IsNullOrEmpty(ItemCast.GoodsFullName))
                            ItemCast.GoodsFullName = ItemCast.XmlGoodsFullName;
                        if (string.IsNullOrEmpty(ItemCast.GoodsDescription))
                            ItemCast.GoodsDescription = ItemCast.XmlGoodsDescription;
                        if (ItemCast.GoodsShelfLifeDays == 0)
                            ItemCast.GoodsShelfLifeDays = ItemCast.XmlGoodsShelfLifeDays;
                        if (string.IsNullOrEmpty(ItemCast.Gtin))
                            ItemCast.Gtin = ItemCast.XmlGtin;
                        if (string.IsNullOrEmpty(ItemCast.Ean13))
                            ItemCast.Ean13 = ItemCast.XmlEan13;
                        if (string.IsNullOrEmpty(ItemCast.Itf14))
                            ItemCast.Itf14 = ItemCast.XmlItf14;
                        if (ItemCast.GoodsBoxQuantly == 0)
                            ItemCast.GoodsBoxQuantly = ItemCast.XmlGoodsBoxQuantly;
                        if (ItemCast.GoodsTareWeight == 0)
                            ItemCast.GoodsTareWeight = ItemCast.CalcGoodsTareWeight();
                        break;
                    case nameof(ItemCast.XmlGoodsName):
                        ItemCast.GoodsName = ItemCast.XmlGoodsName;
                        break;
                    case nameof(ItemCast.XmlGoodsFullName):
                        ItemCast.GoodsFullName = ItemCast.XmlGoodsFullName;
                        break;
                    case nameof(ItemCast.XmlGoodsDescription):
                        ItemCast.GoodsDescription = ItemCast.XmlGoodsDescription;
                        break;
                    case nameof(ItemCast.XmlGoodsShelfLifeDays):
                        ItemCast.GoodsShelfLifeDays = ItemCast.XmlGoodsShelfLifeDays;
                        break;
                    case nameof(ItemCast.XmlGtin):
                        ItemCast.Gtin = ItemCast.XmlGtin;
                        break;
                    case nameof(Barcode.GetGtin):
                        if (ItemCast.Gtin.Length > 12)
                            ItemCast.Gtin = Barcode.GetGtin(ItemCast.Gtin[..13]);
                        break;
                    case nameof(ItemCast.XmlEan13):
                        ItemCast.Ean13 = ItemCast.XmlEan13;
                        break;
                    case nameof(ItemCast.XmlItf14):
                        ItemCast.Itf14 = ItemCast.XmlItf14;
                        break;
                    case nameof(ItemCast.XmlGoodsBoxQuantly):
                        ItemCast.GoodsBoxQuantly = ItemCast.XmlGoodsBoxQuantly;
                        break;
                    case nameof(ItemCast.CalcGoodsTareWeight):
                        ItemCast.GoodsTareWeight = ItemCast.CalcGoodsTareWeight();
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
            //if (ItemCast == null)
            //    return string.Empty;
            XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(ItemCast.Nomenclature.SerializedRepresentationObject);
            // Вес тары = вес коробки + (вес пакета * кол. вложений)
            return $"{ItemCast.CalcGoodWeightBox(xmlProduct)} + ({ItemCast.CalcGoodWeightPack(xmlProduct)} * {ItemCast.CalcGoodRateUnit(xmlProduct)})";
        }

        #endregion
    }
}
