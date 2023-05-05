// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Models;
using WsStorageCore.Tables;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.WorkShops;

namespace WsBlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - OnChange
    
    // TODO: FIX OnChangeItem
    protected void OnChangeItem(WsSqlTableBase? item, string filterName, object? value)
    {
        RunActionsSafe(LocaleCore.Table.TableEdit, () =>
        {
            switch (item)
            {
                case WsSqlAccessModel access:
                    OnChangeItemAccess(access, filterName, value);
                    break;
                case PrinterModel printer:
                    OnChangeItemPrinter(printer, filterName, value);
                    break;
                case WsSqlPrinterResourceFkModel printerResource:
                    OnChangeItemPrinterResource(printerResource, filterName, value);
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
    }

    private void OnChangeItemAccess(WsSqlAccessModel item, string filterName, object? value)
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
            item.PrinterType = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PrinterTypeModel>(printerTypeId);
        }
    }

    private void OnChangeItemPrinterResource(WsSqlPrinterResourceFkModel item, string filterName, object? value)
    {
        switch (filterName)
        {
            case nameof(item.Printer) when value is long printerId:
                item.Printer = ContextManager.AccessManager.AccessItem.GetItemNotNullable<PrinterModel>(printerId);
                break;
            case nameof(item.TemplateResource) when value is long resourceId:
                item.TemplateResource = ContextManager.AccessManager.AccessItem.GetItemNotNullable<TemplateResourceModel>(resourceId);
                break;
        }
    }

    private void OnChangeItemScale(ScaleModel item, string filterName, object? value)
    {
        if (filterName == nameof(ScaleModel.IdentityValueId) && value is long id)
        {
            item = ContextManager.AccessManager.AccessItem.GetItemNotNullable<ScaleModel>(id);
        }
        if (filterName == nameof(ScaleModel.DeviceComPort) && value is string deviceComPort)
        {
            item.DeviceComPort = deviceComPort;
        }
        //if (filterName == nameof(ScaleModel.Host) && value is long hostId)
        //{
        //    item.Host = AppSettings.DataAccess.GetItemById<HostModel>(hostId);
        //}
        if (filterName == nameof(ScaleModel.PrinterMain) && value is long printerId)
        {
            item.PrinterMain = ContextManager.AccessManager.AccessItem.GetItemNullableById<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            item.WorkShop = ContextManager.AccessManager.AccessItem.GetItemNullableById<WorkShopModel>(workShopId);
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
            item.ProductionFacility = ContextManager.AccessManager.AccessItem.GetItemNotNullable<ProductionFacilityModel>(productionFacilityId);
        }
    }

    #endregion
}