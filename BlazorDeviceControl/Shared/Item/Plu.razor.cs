// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using BlazorCore.Models;
using BlazorCore.Models.XML;
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

        private PluEntity PluItem { get { if (PluItem is PluEntity pluEntity) return pluEntity; return null; } }
        [Parameter] public int Id { get => PluItem == null ? 0 : PluItem.Id; set => _ = value; }
        public List<ScalesEntity> ScalesEntities { get; set; } = null;
        public List<TemplatesEntity> TemplatesEntities { get; set; } = null;
        public List<NomenclatureEntity> NomenclatureEntities { get; set; } = null;
        public readonly ProductHelper Product = ProductHelper.Instance;
        private readonly BarcodeHelper _barcode = BarcodeHelper.Instance;

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync(new Task(delegate
            {
                ScalesEntities = null;
                ScalesEntity[] scalesEntities = AppSettings.DataAccess.ScalesCrud.GetEntities(null, null);
                ScalesEntities = new List<ScalesEntity>();
                foreach (ScalesEntity scalesEntity in scalesEntities)
                {
                    ScalesEntities.Add(scalesEntity);
                }

                TemplatesEntities = null;
                TemplatesEntity[] templatesEntities = AppSettings.DataAccess.TemplatesCrud.GetEntities(null, null);
                TemplatesEntities = new List<TemplatesEntity>();
                foreach (TemplatesEntity templatesEntity in templatesEntities)
                {
                    TemplatesEntities.Add(templatesEntity);
                }

                NomenclatureEntities = null;
                NomenclatureEntity[] nomenclatureEntities = AppSettings.DataAccess.NomenclatureCrud.GetEntities(null, null);
                NomenclatureEntities = new List<NomenclatureEntity>();
                foreach (NomenclatureEntity templatesEntity in nomenclatureEntities)
                {
                    NomenclatureEntities.Add(templatesEntity);
                }

                // Проверка шаблона.
                if ((PluItem.Templates == null || PluItem.Templates.EqualsDefault()) && PluItem.Scale.TemplateDefault != null)
                {
                    PluItem.Templates = (TemplatesEntity)PluItem.Scale.TemplateDefault.Clone();
                }

                // Номер PLU.
                if (PluItem.Plu == 0)
                {
                    PluEntity pluEntity = AppSettings.DataAccess.PluCrud.GetEntity(
                        new FieldListEntity(new Dictionary<string, object> { { "Scale.Id", PluItem.Scale.Id } }),
                        new FieldOrderEntity { Direction = EnumOrderDirection.Desc, Name = EnumField.Plu, Use = true });
                    if (pluEntity != null && !pluEntity.EqualsDefault())
                    {
                        PluItem.Plu = pluEntity.Plu + 1;
                    }
                }
            }), false).ConfigureAwait(false);
        }

        private async Task RowSelectAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task RowDoubleClickAsync(BaseIdEntity entity,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            try
            {
                //
            }
            catch (Exception ex)
            {
                NotificationMessage msg = new()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Ошибка метода [{memberName}]!",
                    Detail = ex.Message,
                    Duration = AppSettingsEntity.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
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
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idScale } }),
                                null);
                        }
                        break;
                    case "Nomenclature":
                        if (value is int idNomenclature)
                        {
                            PluItem.Nomenclature = AppSettings.DataAccess.NomenclatureCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idNomenclature } }),
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
                                    new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idTemplate } }),
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

                ProductEntity productEntity = Product.GetProductEntity(PluItem.Nomenclature?.SerializedRepresentationObject);
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
                                    PluItem.Gtin = _barcode.GetGtin(PluItem.Gtin.Substring(0, 13));
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

        private async Task ActionEditAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTableScales table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync<BaseRazorEntity>(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await SetParametersAsync(new ParameterView()).ConfigureAwait(false);
        }

        #endregion
    }
}