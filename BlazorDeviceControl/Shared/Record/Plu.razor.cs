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

namespace BlazorDeviceControl.Shared.Record
{
    public partial class Plu
    {
        #region Public and private fields and properties

        public List<ScalesEntity> ScalesEntities { get; set; } = null;
        public List<TemplatesEntity> TemplatesEntities { get; set; } = null;
        public List<NomenclatureEntity> NomenclatureEntities { get; set; } = null;
        public readonly ProductHelper Product = ProductHelper.Instance;
        private readonly BarcodeHelper _barcode = BarcodeHelper.Instance;
        [Parameter]
        public PluEntity Item { get; set; }

        #endregion

        #region Public and private methods

        public override async Task GetDataAsync()
        {
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
                if ((Item.Templates == null || Item.Templates.EqualsDefault()) && Item.Scale.TemplateDefault != null)
                {
                    Item.Templates = (TemplatesEntity)Item.Scale.TemplateDefault.Clone();
                }

                // Номер PLU.
                if (Item.Plu == 0)
                {
                    PluEntity pluEntity = AppSettings.DataAccess.PluCrud.GetEntity(
                        new FieldListEntity(new Dictionary<string, object> { { "Scale.Id", Item.Scale.Id } }),
                        new FieldOrderEntity { Direction = EnumOrderDirection.Desc, Name = EnumField.Plu, Use = true });
                    if (pluEntity != null && !pluEntity.EqualsDefault())
                    {
                        Item.Plu = pluEntity.Plu + 1;
                    }
                }
            }));
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);

            await GetDataAsync().ConfigureAwait(true);
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
                    Duration = AppSettings.Delay
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
                    Duration = AppSettings.Delay
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
                            Item.Scale = AppSettings.DataAccess.ScalesCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idScale } }),
                                null);
                        }
                        break;
                    case "Nomenclature":
                        if (value is int idNomenclature)
                        {
                            Item.Nomenclature = AppSettings.DataAccess.NomenclatureCrud.GetEntity(
                                new FieldListEntity(new Dictionary<string, object> { { EnumField.Id.ToString(), idNomenclature } }),
                                null);
                            OnClickFieldsFill("Entity");
                        }
                        break;
                    case "Templates":
                        if (value is int idTemplate)
                        {
                            if (idTemplate <= 0)
                                Item.Templates = null;
                            else
                            {
                                Item.Templates = AppSettings.DataAccess.TemplatesCrud.GetEntity(
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
                    Duration = AppSettings.Delay
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
                if (Item.Nomenclature == null)
                    return;
                if (name.Equals("clear", StringComparison.InvariantCultureIgnoreCase))
                {
                    Item.Nomenclature = null;
                    Item.GoodsName = string.Empty;
                    Item.GoodsFullName = string.Empty;
                    Item.GoodsDescription = string.Empty;
                    Item.GoodsShelfLifeDays = 0;
                    Item.Gtin = string.Empty;
                    Item.Ean13 = string.Empty;
                    Item.Itf14 = string.Empty;
                    Item.GoodsBoxQuantly = 0;
                    Item.GoodsTareWeight = 0;
                }

                ProductEntity productEntity = Product.GetProductEntity(Item.Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    if (name.Equals("Entity", StringComparison.InvariantCultureIgnoreCase))
                    {
                        if (string.IsNullOrEmpty(Item.GoodsName))
                            Item.GoodsName = Item.XmlGoodsName;
                        if (string.IsNullOrEmpty(Item.GoodsFullName))
                            Item.GoodsFullName = Item.XmlGoodsFullName;
                        if (string.IsNullOrEmpty(Item.GoodsDescription))
                            Item.GoodsDescription = Item.XmlGoodsDescription;
                        if (Item.GoodsShelfLifeDays == 0)
                            Item.GoodsShelfLifeDays = Item.XmlGoodsShelfLifeDays;
                        if (string.IsNullOrEmpty(Item.Gtin))
                            Item.Gtin = Item.XmlGtin;
                        if (string.IsNullOrEmpty(Item.Ean13))
                            Item.Ean13 = Item.XmlEan13;
                        if (string.IsNullOrEmpty(Item.Itf14))
                            Item.Itf14 = Item.XmlItf14;
                        if (Item.GoodsBoxQuantly == 0)
                            Item.GoodsBoxQuantly = Item.XmlGoodsBoxQuantly;
                        if (Item.GoodsTareWeight == 0)
                            Item.GoodsTareWeight = Item.CalcGoodsTareWeight();
                    }
                    else
                    {
                        switch (name.ToLower())
                        {
                            case "goodsname":
                                Item.GoodsName = Item.XmlGoodsName;
                                break;
                            case "goodsfullname":
                                Item.GoodsFullName = Item.XmlGoodsFullName;
                                break;
                            case "goodsdescription":
                                Item.GoodsDescription = Item.XmlGoodsDescription;
                                break;
                            case "goodsshelflifedays":
                                Item.GoodsShelfLifeDays = Item.XmlGoodsShelfLifeDays;
                                break;
                            case "gtin":
                                Item.Gtin = Item.XmlGtin;
                                break;
                            case "getgtin":
                                if (Item.Gtin.Length > 12)
                                    Item.Gtin = _barcode.GetGtin(Item.Gtin.Substring(0, 13));
                                break;
                            case "ean13":
                                Item.Ean13 = Item.XmlEan13;
                                break;
                            case "itf14":
                                Item.Itf14 = Item.XmlItf14;
                                break;
                            case "goodsboxquantly":
                                Item.GoodsBoxQuantly = Item.XmlGoodsBoxQuantly;
                                break;
                            case "goodstareweight":
                                Item.GoodsTareWeight = Item.CalcGoodsTareWeight();
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
                    Duration = AppSettings.Delay
                };
                Notification.Notify(msg);
                Console.WriteLine($"{msg.Summary}. {msg.Detail}");
                AppSettings.DataAccess.LogExceptionToSql(ex, filePath, lineNumber, memberName);
            }
        }

        private async Task ActionEditAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Edit, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionAddAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Add, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionCopyAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Copy, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        private async Task ActionDeleteAsync(EnumTable table, BaseIdEntity entity, BaseIdEntity parentEntity)
        {
            await ActionAsync(table, EnumTableAction.Delete, entity, parentEntity).ConfigureAwait(true);
            await GetDataAsync().ConfigureAwait(false);
        }

        #endregion
    }
}