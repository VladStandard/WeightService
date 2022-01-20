// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared
{
    public partial class EntityPage
    {
        #region Public and private fields and properties

        [Parameter] public ShareEnums.DbTableAction TableAction { get; set; }
        [Parameter] public EventCallback CallbackActionSaveAsync { get; set; }
        [Parameter] public EventCallback CallbackActionCancelAsync { get; set; }

        #endregion

        #region Public and private methods

        private async Task SaveAsync(MouseEventArgs args)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //bool success = true;
            //    switch (Table)
            //    {
            //        case ProjectsEnums.TableScale.Contragents:
            //            ContragentEntity contragents = (ContragentEntity)Item;
            //            contragents.CreateDate ??= DateTime.Now;
            //            contragents.ModifiedDate = DateTime.Now;
            //            if (TableAction == ShareEnums.DbTableAction.Add)
            //                AppSettings.DataAccess.ContragentsCrud.SaveEntity(contragents);
            //            else
            //                AppSettings.DataAccess.ContragentsCrud.UpdateEntity(contragents);
            //            break;
            //        case ProjectsEnums.TableScale.Nomenclatures:
            //            NomenclatureEntity nomenclature = (NomenclatureEntity)Item;
            //            nomenclature.CreateDate ??= DateTime.Now;
            //            nomenclature.ModifiedDate = DateTime.Now;
            //            if (TableAction == ShareEnums.DbTableAction.Add)
            //                AppSettings.DataAccess.NomenclaturesCrud.SaveEntity(nomenclature);
            //            else
            //                AppSettings.DataAccess.NomenclaturesCrud.UpdateEntity(nomenclature);
            //            break;
            //        case ProjectsEnums.TableScale.Plus:
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
            //                    { ShareEnums.DbField.Plu.ToString(), plu.Plu },
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
            //                    if (TableAction == ShareEnums.DbTableAction.Add)
            //                        AppSettings.DataAccess.PlusCrud.SaveEntity(plu);
            //                    else
            //                        AppSettings.DataAccess.PlusCrud.UpdateEntity(plu);
            //                }
            //            }
            //            break;
            //        case ProjectsEnums.TableScale.ProductionFacilities:
            //            ProductionFacilityEntity productionFacility = (ProductionFacilityEntity)Item;
            //            productionFacility.CreateDate ??= DateTime.Now;
            //            productionFacility.ModifiedDate = DateTime.Now;
            //            if (TableAction == ShareEnums.DbTableAction.Add)
            //                AppSettings.DataAccess.Crud.SaveEntity(productionFacility);
            //            else
            //                AppSettings.DataAccess.Crud.UpdateEntity(productionFacility);
            //            break;
            //        case ProjectsEnums.TableScale.ProductSeries:
            //            ProductSeriesEntity productSeries = (ProductSeriesEntity)Item;
            //            productSeries.CreateDate ??= DateTime.Now;
            //            if (TableAction == ShareEnums.DbTableAction.Add)
            //                AppSettings.DataAccess.ProductSeriesCrud.SaveEntity(productSeries);
            //            else
            //                AppSettings.DataAccess.ProductSeriesCrud.UpdateEntity(productSeries);
            //            break;
            //        case ProjectsEnums.TableScale.TemplateResources:
            //            TemplateResourceEntity templateResourcesEntity = (TemplateResourceEntity)Item;
            //            templateResourcesEntity.CreateDate ??= DateTime.Now;
            //            templateResourcesEntity.ModifiedDate = DateTime.Now;
            //            if (TableAction == ShareEnums.DbTableAction.Add)
            //                AppSettings.DataAccess.TemplateResourcesCrud.SaveEntity(templateResourcesEntity);
            //            else
            //                AppSettings.DataAccess.TemplateResourcesCrud.UpdateEntity(templateResourcesEntity);
            //            break;
            //        case ProjectsEnums.TableScale.Templates:
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
            //                if (TableAction is ShareEnums.DbTableAction.Add or ShareEnums.DbTableAction.Copy)
            //                {
            //                    AppSettings.DataAccess.TemplatesCrud.SaveEntity(templateEntity);
            //                }
            //                else
            //                {
            //                    AppSettings.DataAccess.TemplatesCrud.UpdateEntity(templateEntity);
            //                }
            //            }
            //            break;
            //    }
            DialogService.Close(true);
        }

        #endregion
    }
}
