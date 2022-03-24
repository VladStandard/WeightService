// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.DataModels;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Plu
    {
        #region Public and private fields and properties

        public PluEntity? ItemCast { get => Item == null ? null : (PluEntity)Item; set => Item = value; }
        public List<ScaleEntity>? ScaleItems { get; set; } = null;
        public List<TemplateEntity>? TemplateItems { get; set; } = null;
        public List<NomenclatureEntity>? NomenclatureItems { get; set; } = null;
        private XmlProductHelper ProductHelper { get; set; } = XmlProductHelper.Instance;
        private BarcodeHelper Barcode { get; set; } = BarcodeHelper.Instance;
        private readonly object _locker = new();
        public string PageSelf => ItemCast == null ? string.Empty : $"{@LocalizationData.DeviceControl.UriRouteItem.Plu}/{ItemCast.Id}";
        public string PageScale =>
            ItemCast?.Scale == null ? PageSelf : $"{@LocalizationData.DeviceControl.UriRouteItem.Scale}/{ItemCast.Scale.Id}";
        public string PageTemplate =>
            ItemCast?.Template == null ? PageSelf : $"{@LocalizationData.DeviceControl.UriRouteItem.Template}/{ItemCast.Template.Id}";
        public string PageNomenclature =>
            ItemCast?.Nomenclature == null ? PageSelf : $"{@LocalizationData.DeviceControl.UriRouteItem.Nomenclature}/{ItemCast.Nomenclature.Id}";

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
                        ItemCast = null;
                        ScaleItems = null;
                        TemplateItems = null;
                        NomenclatureItems = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        ItemCast = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(
                            new FieldListEntity(new Dictionary<string, object?>{ { DbField.Id.ToString(), Id } }), null);
                        if (Id != null && TableAction == DbTableAction.New)
                            ItemCast.Id = (long)Id;
                        ScaleItems = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }), 
                            null)?.ToList();
                        TemplateItems = AppSettings.DataAccess.Crud.GetEntities<TemplateEntity>(
                            new FieldListEntity(new Dictionary<string, object?> { { DbField.Marked.ToString(), false } }),
                            null)?.ToList();
                        NomenclatureItems = AppSettings.DataAccess.Crud.GetEntities<NomenclatureEntity>(
                            null,
                            null)?.ToList();
                        //// Проверка шаблона.
                        //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
                        //{
                        //    PluItem.Templates = (TemplateEntity)PluItem.Scale.TemplateDefault.Clone();
                        //}
                        //// Номер PLU.
                        //if (PluItem.Plu == 0)
                        //{
                        //    PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
                        //        new FieldListEntity(new Dictionary<string, object,> { { "Scale.Id", PluItem.Scale.Id } }),
                        //        new FieldOrderEntity { Direction = DbOrderDirection.Desc, Name = DbField.Plu, Use = true });
                        //    if (pluEntity != null && !pluEntity.EqualsDefault())
                        //    {
                        //        PluItem.Plu = pluEntity.Plu + 1;
                        //    }
                        ButtonSettings = new(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void OnChange(object value, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                lock (_locker)
                {
                    switch (name)
                    {
                        case "Scale":
                            if (ItemCast != null && value is long idScale)
                            {
                                ItemCast.Scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> { { DbField.Id.ToString(), idScale } }),
                                    null);
                            }
                            break;
                        case "Nomenclature":
                            if (ItemCast != null && value is long idNomenclature)
                            {
                                ItemCast.Nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> { { DbField.Id.ToString(), idNomenclature } }),
                                    null);
                                OnClickFieldsFill("Entity");
                            }
                            break;
                        case "Templates":
                            if (ItemCast != null && value is long idTemplate)
                            {
                                if (idTemplate <= 0)
                                    ItemCast.Template = new();
                                else
                                {
                                    ItemCast.Template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
                                        new FieldListEntity(new Dictionary<string, object?> { { DbField.Id.ToString(), idTemplate } }),
                                        null);
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{nameof(OnChange)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                NotificationService.Notify(msg);
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                StateHasChanged();
            }
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
                    case nameof(LocalizationData.DeviceControl.TableActionClear):
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
                    case nameof(LocalizationData.DeviceControl.TableActionFill):
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
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"{LocalizationCore.Strings.Main.MethodError} [{nameof(OnClickFieldsFill)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsHelper.Delay
                };
                NotificationService.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.Crud.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private string GetWeightFormula()
        {
            if (ItemCast == null)
                return string.Empty;
            XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(ItemCast.Nomenclature.SerializedRepresentationObject);
            // Вес тары = вес коробки + (вес пакета * кол. вложений)
            return $"{ItemCast.CalcGoodWeightBox(xmlProduct)} + ({ItemCast.CalcGoodWeightPack(xmlProduct)} * {ItemCast.CalcGoodRateUnit(xmlProduct)})";
        }

        #endregion
    }
}
