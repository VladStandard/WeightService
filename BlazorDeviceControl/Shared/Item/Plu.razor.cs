// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorProjectsCore.Models;
using DataProjectsCore;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
using Radzen;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class Plu
    {
        #region Public and private fields and properties

        public PluEntity PluItem { get => (PluEntity)IdItem; set => SetItem(value); }
        public List<ScaleEntity> ScaleItems { get; set; } = null;
        public List<TemplateEntity> TemplateItems { get; set; } = null;
        public List<NomenclatureEntity> NomenclatureItems { get; set; } = null;
        private XmlProductHelper Product { get; set; } = XmlProductHelper.Instance;
        private BarcodeHelper Barcode { get; set; } = BarcodeHelper.Instance;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
                        IdItem = null;
                        ScaleItems = null;
                        TemplateItems = null;
                        NomenclatureItems = null;
                        await GuiRefreshWithWaitAsync();

                        //ScaleEntity[] scalesEntities = AppSettings.DataAccess.ScalesCrud.GetEntities(null, null);
                        //ScaleItems = new List<ScaleEntity>();
                        //foreach (ScaleEntity scalesEntity in scalesEntities)
                        //{
                        //    ScaleItems.Add(scalesEntity);
                        //}

                        //TemplateEntity[] templatesEntities = AppSettings.DataAccess.TemplatesCrud.GetEntities(null, null);
                        //TemplateItems = new List<TemplateEntity>();
                        //foreach (TemplateEntity templatesEntity in templatesEntities)
                        //{
                        //    TemplateItems.Add(templatesEntity);
                        //}

                        //NomenclatureEntity[] nomenclatureEntities = AppSettings.DataAccess.NomenclaturesCrud.GetEntities(null, null);
                        //NomenclatureItems = new List<NomenclatureEntity>();
                        //foreach (NomenclatureEntity templatesEntity in nomenclatureEntities)
                        //{
                        //    NomenclatureItems.Add(templatesEntity);
                        //}

                        //// Проверка шаблона.
                        //if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
                        //{
                        //    PluItem.Templates = (TemplateEntity)PluItem.Scale.TemplateDefault.Clone();
                        //}

                        //// Номер PLU.
                        //if (PluItem.Plu == 0)
                        //{
                        //    PluEntity pluEntity = AppSettings.DataAccess.PlusCrud.GetEntity(
                        //        new FieldListEntity(new Dictionary<string, object> { { "Scale.Id", PluItem.Scale.Id } }),
                        //        new FieldOrderEntity { Direction = ShareEnums.DbOrderDirection.Desc, Name = ShareEnums.DbField.Plu, Use = true });
                        //    if (pluEntity != null && !pluEntity.EqualsDefault())
                        //    {
                        //        PluItem.Plu = pluEntity.Plu + 1;
                        //    }
                        //}
                        await GuiRefreshWithWaitAsync();
                    }),
                }, true);
        }

        private void OnChange(object value, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                switch (name)
                {
                    case "Scale":
                        if (value is int idScale)
                        {
                            PluItem.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idScale } }),
                                null);
                        }
                        break;
                    case "Nomenclature":
                        if (value is int idNomenclature)
                        {
                            PluItem.Nomenclature = AppSettings.DataAccess.NomenclaturesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idNomenclature } }),
                                null);
                            OnClickFieldsFill("Entity");
                        }
                        break;
                    case "Templates":
                        if (value is int idTemplate)
                        {
                            if (idTemplate <= 0)
                                PluItem.Templates = null;
                            else
                            {
                                PluItem.Templates = AppSettings.DataAccess.TemplatesCrud.GetEntity(
                                    new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Id.ToString(), idTemplate } }),
                                    null);
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{nameof(OnChange)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
            finally
            {
                StateHasChanged();
            }
        }

        private void OnClickFieldsFill(string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                if (PluItem.Nomenclature == null)
                    return;
                if (name.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
                {
                    PluItem.Nomenclature = null;
                    PluItem.GoodsName = string.Empty;
                    PluItem.GoodsFullName = string.Empty;
                    PluItem.GoodsDescription = string.Empty;
                    PluItem.GoodsShelfLifeDays = 0;
                    PluItem.Gtin = string.Empty;
                    PluItem.Ean13 = string.Empty;
                    PluItem.Itf14 = string.Empty;
                    PluItem.GoodsBoxQuantly = 0;
                    PluItem.GoodsTareWeight = 0;
                }

                XmlProductEntity productEntity = Product.GetProductEntity(PluItem.Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    if (name.Equals("Entity", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(PluItem.GoodsName))
                            PluItem.GoodsName = PluItem.XmlGoodsName;
                        if (string.IsNullOrEmpty(PluItem.GoodsFullName))
                            PluItem.GoodsFullName = PluItem.XmlGoodsFullName;
                        if (string.IsNullOrEmpty(PluItem.GoodsDescription))
                            PluItem.GoodsDescription = PluItem.XmlGoodsDescription;
                        if (PluItem.GoodsShelfLifeDays == 0)
                            PluItem.GoodsShelfLifeDays = PluItem.XmlGoodsShelfLifeDays;
                        if (string.IsNullOrEmpty(PluItem.Gtin))
                            PluItem.Gtin = PluItem.XmlGtin;
                        if (string.IsNullOrEmpty(PluItem.Ean13))
                            PluItem.Ean13 = PluItem.XmlEan13;
                        if (string.IsNullOrEmpty(PluItem.Itf14))
                            PluItem.Itf14 = PluItem.XmlItf14;
                        if (PluItem.GoodsBoxQuantly == 0)
                            PluItem.GoodsBoxQuantly = PluItem.XmlGoodsBoxQuantly;
                        if (PluItem.GoodsTareWeight == 0)
                            PluItem.GoodsTareWeight = PluItem.CalcGoodsTareWeight();
                    }
                    else
                    {
                        switch (name.ToLower())
                        {
                            case "goodsname":
                                PluItem.GoodsName = PluItem.XmlGoodsName;
                                break;
                            case "goodsfullname":
                                PluItem.GoodsFullName = PluItem.XmlGoodsFullName;
                                break;
                            case "goodsdescription":
                                PluItem.GoodsDescription = PluItem.XmlGoodsDescription;
                                break;
                            case "goodsshelflifedays":
                                PluItem.GoodsShelfLifeDays = PluItem.XmlGoodsShelfLifeDays;
                                break;
                            case "gtin":
                                PluItem.Gtin = PluItem.XmlGtin;
                                break;
                            case "getgtin":
                                if (PluItem.Gtin.Length > 12)
                                    PluItem.Gtin = Barcode.GetGtin(PluItem.Gtin.Substring(0, 13));
                                break;
                            case "ean13":
                                PluItem.Ean13 = PluItem.XmlEan13;
                                break;
                            case "itf14":
                                PluItem.Itf14 = PluItem.XmlItf14;
                                break;
                            case "goodsboxquantly":
                                PluItem.GoodsBoxQuantly = PluItem.XmlGoodsBoxQuantly;
                                break;
                            case "goodstareweight":
                                PluItem.GoodsTareWeight = PluItem.CalcGoodsTareWeight();
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{nameof(OnClickFieldsFill)}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        #endregion
    }
}
