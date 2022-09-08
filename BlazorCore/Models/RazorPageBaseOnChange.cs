// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.Core;
using Microsoft.AspNetCore.Components;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
{
    #region Public and private methods - OnChange

    protected void OnChange() => ActionChange.Invoke();
    protected void OnChangeAsync() => InvokeAsync(ActionChange);

    protected void OnChangeItem(DataCore.Sql.Tables.TableBase? item, string filterName, object? value)
    {
        RunActionsSafe(LocaleCore.Table.TableEdit, LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                switch (item)
                {
                    case AccessModel access:
                        OnChangeItemAccess(access, filterName, value);
                        break;
                    case PrinterModel printer:
                        OnChangeItemPrinter(printer, filterName, value);
                        break;
                    case PrinterResourceModel printerResource:
                        OnChangeItemPrinterResource(printerResource, filterName, value);
                        break;
                    case PluObsoleteModel pluObsolete:
                        OnChangeItemPlu(pluObsolete, filterName, value);
                        break;
                    case ScaleModel scale:
                        OnChangeItemScale(scale, filterName, value);
                        break;
                    case TemplateModel template:
                        OnChangeItemTemplate(template, filterName, value);
                        break;
                    case WorkShopModel workShop:
                        OnChangeItemWorkShop(workShop, filterName, value);
                        break;
                }
            });
        OnChange();
    }

    private void OnChangeItemAccess(AccessModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.Rights) && value is AccessRightsEnum rights)
        {
            item.Rights = (byte)rights;
        }
    }

    private void OnChangeItemPrinter(PrinterModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.PrinterType) && value is long printerTypeId)
        {
            item.PrinterType = AppSettings.DataAccess.GetItemById<PrinterTypeModel>(printerTypeId) ?? new();
        }
    }

    private void OnChangeItemPrinterResource(PrinterResourceModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.Printer) && value is long printerId)
        {
            item.Printer = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId) ?? new();
        }
        if (filterName == nameof(item.Resource) && value is long resourceId)
        {
            item.Resource = AppSettings.DataAccess.GetItemById<TemplateResourceModel>(resourceId) ?? new();
        }
    }

    private void OnChangeItemPlu(PluObsoleteModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.Nomenclature) && value is long nomenclatureId)
        {
            item.Nomenclature = AppSettings.DataAccess.GetItemById<NomenclatureModel>(nomenclatureId) ?? new();
        }
        if (filterName == nameof(item.Scale) && value is long scaleId)
        {
            item.Scale = AppSettings.DataAccess.GetItemById<ScaleModel>(scaleId) ?? new();
        }
        if (filterName == nameof(item.Template) && value is long templateId)
        {
            item.Template = AppSettings.DataAccess.GetItemById<TemplateModel>(templateId) ?? new();
        }
    }

    private void OnChangeItemScale(ScaleModel item, string filterName, object? value)
    {
        if (filterName == nameof(ScaleModel.Identity.Id) && value is long id)
        {
            item = AppSettings.DataAccess.GetItemById<ScaleModel>(id) ?? new();
        }
        if (filterName == nameof(ScaleModel.DeviceComPort) && value is string deviceComPort)
        {
            item.DeviceComPort = deviceComPort;
        }
        if (filterName == nameof(ScaleModel.Host) && value is long hostId)
        {
            item.Host = AppSettings.DataAccess.GetItemById<HostModel>(hostId);
        }
        if (filterName == nameof(ScaleModel.TemplateDefault) && value is long templateDefaultId)
        {
            item.TemplateDefault = AppSettings.DataAccess.GetItemById<TemplateModel>(templateDefaultId);
        }
        if (filterName == nameof(ScaleModel.TemplateSeries) && value is long templateSeriesId)
        {
            item.TemplateSeries = AppSettings.DataAccess.GetItemById<TemplateModel>(templateSeriesId);
        }
        if (filterName == nameof(ScaleModel.PrinterMain) && value is long printerId)
        {
            item.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            item.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(workShopId);
        }
    }

    private void OnChangeItemTemplate(TemplateModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.CategoryId) && value is string categoryId)
        {
            item.CategoryId = categoryId;
        }
    }

    private void OnChangeItemWorkShop(WorkShopModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.ProductionFacility) && value is int productionFacilityId)
        {
            item.ProductionFacility = AppSettings.DataAccess.GetItemById<ProductionFacilityModel>(productionFacilityId) ?? new();
        }
    }

    #endregion
}
