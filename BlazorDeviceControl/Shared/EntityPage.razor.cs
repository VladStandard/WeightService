// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore;
using BlazorCore.DAL;
using DataCore.DAL.TableModels;
using DataCore.DAL.TableSystemModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class EntityPage
    {
        #region Public and private fields and properties

        [Parameter] public EnumTableAction TableAction { get; set; }
        [Parameter] public EventCallback CallbackActionSaveAsync { get; set; }
        [Parameter] public EventCallback CallbackActionCancelAsync { get; set; }

        #endregion

        #region Public and private methods

        private async Task SaveAsync(MouseEventArgs args,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //bool success = true;
            //    switch (Table)
            //    {
            //        case EnumTableScale.Hosts:
            //            HostEntity hosts = (HostEntity)Item;
            //            hosts.CreateDate ??= DateTime.Now;
            //            hosts.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.HostsCrud.SaveEntity(hosts);
            //            else
            //                AppSettings.DataAccess.HostsCrud.UpdateEntity(hosts);
            //            break;
            //        case EnumTableScale.Contragents:
            //            ContragentEntity contragents = (ContragentEntity)Item;
            //            contragents.CreateDate ??= DateTime.Now;
            //            contragents.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.ContragentsCrud.SaveEntity(contragents);
            //            else
            //                AppSettings.DataAccess.ContragentsCrud.UpdateEntity(contragents);
            //            break;
            //        case EnumTableScale.Nomenclatures:
            //            NomenclatureEntity nomenclature = (NomenclatureEntity)Item;
            //            nomenclature.CreateDate ??= DateTime.Now;
            //            nomenclature.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.NomenclaturesCrud.SaveEntity(nomenclature);
            //            else
            //                AppSettings.DataAccess.NomenclaturesCrud.UpdateEntity(nomenclature);
            //            break;
            //        case EnumTableScale.Plus:
            //            PluEntity plu = (PluEntity)Item;
            //            plu.ModifiedDate = DateTime.Now;
            //            success = FieldControlDeny(plu.Scale, "Устройство");
            //            if (success)
            //                success = FieldControlDeny(plu.Templates, "Шаблон");
            //            if (success)
            //                success = FieldControlDeny(plu.Nomenclature, "Номенклатура");
            //            if (success)
            //            {
            //                // Контроль номера PLU.
            //                PluEntity[] pluEntities = AppSettings.DataAccess.PlusCrud.GetEntities(
            //                    new FieldListEntity(new Dictionary<string, object>
            //                    {
            //                    { "Scale.Id", plu.Scale.Id },
            //                    { EnumField.Plu.ToString(), plu.Plu },
            //                                                        }),
            //                    null);
            //                if (pluEntities.Any() && !pluEntities.Where(x => x.Id.Equals(Item.Id)).Select(x => x).Any())
            //                {
            //                    NotificationMessage msg = new()
            //                    {
            //                        Severity = NotificationSeverity.Warning,
            //                        Summary = "Контроль данных",
            //                        Detail = $"Таблица PLU уже имеет такой номер [{plu.Plu}]!",
            //                        Duration = BlazorCore.Models.AppSettingsEntity.Delay
            //                    };
            //                    Notification.Notify(msg);
            //                }
            //                else
            //                {
            //                    if (TableAction == EnumTableAction.Add)
            //                        AppSettings.DataAccess.PlusCrud.SaveEntity(plu);
            //                    else
            //                        AppSettings.DataAccess.PlusCrud.UpdateEntity(plu);
            //                }
            //            }
            //            break;
            //        case EnumTableScale.ProductionFacilities:
            //            ProductionFacilityEntity productionFacility = (ProductionFacilityEntity)Item;
            //            productionFacility.CreateDate ??= DateTime.Now;
            //            productionFacility.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.ProductionFacilitiesCrud.SaveEntity(productionFacility);
            //            else
            //                AppSettings.DataAccess.ProductionFacilitiesCrud.UpdateEntity(productionFacility);
            //            break;
            //        case EnumTableScale.ProductSeries:
            //            ProductSeriesEntity productSeries = (ProductSeriesEntity)Item;
            //            productSeries.CreateDate ??= DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.ProductSeriesCrud.SaveEntity(productSeries);
            //            else
            //                AppSettings.DataAccess.ProductSeriesCrud.UpdateEntity(productSeries);
            //            break;
            //        case EnumTableScale.TemplateResources:
            //            TemplateResourceEntity templateResourcesEntity = (TemplateResourceEntity)Item;
            //            templateResourcesEntity.CreateDate ??= DateTime.Now;
            //            templateResourcesEntity.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.TemplateResourcesCrud.SaveEntity(templateResourcesEntity);
            //            else
            //                AppSettings.DataAccess.TemplateResourcesCrud.UpdateEntity(templateResourcesEntity);
            //            break;
            //        case EnumTableScale.Templates:
            //            TemplateEntity templateEntity = (TemplateEntity)Item;
            //            if (string.IsNullOrEmpty(templateEntity.CategoryId))
            //            {
            //                success = false;
            //                NotificationMessage msg = new()
            //                {
            //                    Severity = NotificationSeverity.Warning,
            //                    Summary = "Контроль данных",
            //                    Detail = "Необходимо заполнить поле [Категория]!",
            //                    Duration = BlazorCore.Models.AppSettingsEntity.Delay
            //                };
            //                Notification.Notify(msg);
            //            }
            //            if (success)
            //            {
            //                templateEntity.CreateDate ??= DateTime.Now;
            //                templateEntity.ModifiedDate = DateTime.Now;
            //                if (TableAction is EnumTableAction.Add or EnumTableAction.Copy)
            //                {
            //                    AppSettings.DataAccess.TemplatesCrud.SaveEntity(templateEntity);
            //                }
            //                else
            //                {
            //                    AppSettings.DataAccess.TemplatesCrud.UpdateEntity(templateEntity);
            //                }
            //            }
            //            break;
            //        case EnumTableScale.WeithingFacts:
            //            break;
            //        case EnumTableScale.Workshops:
            //            WorkshopEntity workshopEntity = (WorkshopEntity)Item;
            //            workshopEntity.CreateDate ??= DateTime.Now;
            //            workshopEntity.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //                AppSettings.DataAccess.WorkshopsCrud.SaveEntity(workshopEntity);
            //            else
            //                AppSettings.DataAccess.WorkshopsCrud.UpdateEntity(workshopEntity);
            //            break;
            //        case EnumTableScale.PrinterResources:
            //            ZebraPrinterResourceEntity zebraPrinterResourceRefEntity = (ZebraPrinterResourceEntity)Item;
            //            zebraPrinterResourceRefEntity.CreateDate = DateTime.Now;
            //            zebraPrinterResourceRefEntity.ModifiedDate = DateTime.Now;
            //            if (TableAction == EnumTableAction.Add)
            //            {
            //                AppSettings.DataAccess.PrinterResourcesCrud.SaveEntity(zebraPrinterResourceRefEntity);
            //            }
            //            else
            //            {
            //                bool existsEntity = AppSettings.DataAccess.PrinterResourcesCrud.ExistsEntity(
            //                    new FieldListEntity(new Dictionary<string, object>
            //                    {{EnumField.Id.ToString(), zebraPrinterResourceRefEntity.Id}}),
            //                    new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
            //                if (existsEntity)
            //                {
            //                    int idLast = AppSettings.DataAccess.PrinterResourcesCrud.GetEntity(
            //                        new FieldListEntity(new Dictionary<string, object>
            //                        { { "Printer.Id", zebraPrinterResourceRefEntity.Printer.Id }}),
            //                        new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc)).Id;
            //                    //zebraPrinterResourceRefEntity.Id = idLast + 1;
            //                    AppSettings.DataAccess.PrinterResourcesCrud.UpdateEntity(zebraPrinterResourceRefEntity);
            //                }
            //                else
            //                {
            //                    AppSettings.DataAccess.PrinterResourcesCrud.UpdateEntity(zebraPrinterResourceRefEntity);
            //                }
            //            }
            //            break;
            //    }
            Dialog.Close(true);
        }

        #endregion
    }
}
