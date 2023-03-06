// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace BlazorCore.Razors;

public partial class RazorComponentBase
{
    #region Public and private methods - OnChange

    protected virtual void OnChangeAdditional() {}

	protected void OnChange() => OnChangeParent(this);

    protected void OnChangeAsync() => OnChangeParentAsync(this);

    private void OnChangeParent(RazorComponentBase? razorPage)
    {
	    while (true)
	    {
		    if (razorPage is null) return;
		    razorPage.OnChangeAdditional();
			razorPage.StateHasChanged();
		    razorPage.OnParametersSet();
            
		    razorPage = razorPage.ParentRazor;
	    }
    }

    private void OnChangeParentAsync(RazorComponentBase? razorPage)
    {
	    while (true)
	    {
		    if (razorPage is null) return;
		    InvokeAsync(razorPage.OnChangeAdditional);
			InvokeAsync(razorPage.StateHasChanged);
            InvokeAsync(razorPage.OnParametersSet);
            
			razorPage = razorPage.ParentRazor;
	    }
    }

    // TODO: FIX OnChangeItem
    protected void OnChangeItem(SqlTableBase? item, string filterName, object? value)
    {
        RunActionsSafe(LocaleCore.Table.TableEdit, () =>
        {
            switch (item)
            {
                case AccessModel access:
                    OnChangeItemAccess(access, filterName, value);
                    break;
                case PrinterModel printer:
                    OnChangeItemPrinter(printer, filterName, value);
                    break;
                case PrinterResourceFkModel printerResource:
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
            item.PrinterType = DataContext.GetItemNotNullable<PrinterTypeModel>(printerTypeId);
        }
    }

    private void OnChangeItemPrinterResource(PrinterResourceFkModel item, string filterName, object? value)
    {
        if (filterName == nameof(item.Printer) && value is long printerId)
        {
            item.Printer = DataContext.GetItemNotNullable<PrinterModel>(printerId);
        }
        if (filterName == nameof(item.TemplateResource) && value is long resourceId)
        {
            item.TemplateResource = DataContext.GetItemNotNullable<TemplateResourceModel>(resourceId);
        }
    }

    private void OnChangeItemScale(ScaleModel item, string filterName, object? value)
    {
        if (filterName == nameof(ScaleModel.IdentityValueId) && value is long id)
        {
            item = DataContext.GetItemNotNullable<ScaleModel>(id);
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
            item.PrinterMain = DataContext.GetItemNullable<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            item.WorkShop = DataContext.GetItemNullable<WorkShopModel>(workShopId);
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
            item.ProductionFacility = DataContext.GetItemNotNullable<ProductionFacilityModel>(productionFacilityId);
        }
    }

    #endregion
}