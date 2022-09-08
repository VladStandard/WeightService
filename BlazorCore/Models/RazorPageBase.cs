// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Models;
using DataCore.Protocols;
using DataCore.Sql.Core;
using DataCore.Sql.Models;
using DataCore.Sql.Tables;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Sockets;
using Environment = System.Environment;

namespace BlazorCore.Models;

public partial class RazorPageBase : LayoutComponentBase
{
    #region Public and private methods

    protected void OnChangeCheckBox(object value, string name)
    {
        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                switch (name)
                {
                    case nameof(RazorConfig.IsShowMarked):
                        if (value is bool isShowMarkedItems)
	                        RazorConfig.IsShowMarked = isShowMarkedItems;
                        break;
                    case nameof(RazorConfig.IsShowOnlyTop):
                        if (value is bool isShowTOp100)
	                        RazorConfig.IsShowMarked = isShowTOp100;
                        break;
                }
            });
        ParentRazor?.OnChange();
    }

    public void OnItemValueChange(DataCore.Sql.Tables.TableBase? item, string? filterName, object? value)
    {
        RunActionsSafe(nameof(OnItemValueChange), LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                switch (item)
                {
                    case AccessModel access:
                        OnItemValueChangeAccess(filterName, value, access);
                        break;
                    case PrinterModel printer:
                        OnItemValueChangePrinter(filterName, value, printer);
                        break;
                    case PrinterResourceModel printerResource:
                        OnItemValueChangePrinterResource(filterName, value, printerResource);
                        break;
                    case PluObsoleteModel pluObsolete:
                        OnItemValueChangePlu(filterName, value, pluObsolete);
                        break;
                    case ScaleModel scale:
                        OnItemValueChangeScale(filterName, value, scale);
                        break;
                    case TemplateModel template:
                        OnItemValueChangeTemplate(filterName, value, template);
                        break;
                    case WorkShopModel workShop:
                        OnItemValueChangeWorkShop(filterName, value, workShop);
                        break;
                }
            });

        ParentRazor?.OnChange();
    }

    private void OnItemValueChangeAccess(string? filterName, object? value, AccessModel access)
    {
        if (filterName == nameof(access.Rights) && value is AccessRightsEnum rights)
        {
            access.Rights = (byte)rights;
        }
    }

    private void OnItemValueChangePrinter(string? filterName, object? value, PrinterModel printer)
    {
        if (filterName == nameof(printer.PrinterType) && value is long printerTypeId)
        {
            printer.PrinterType = AppSettings.DataAccess.GetItemById<PrinterTypeModel>(printerTypeId) ?? new();
        }
    }

    private void OnItemValueChangePrinterResource(string? filterName, object? value, PrinterResourceModel printerResource)
    {
        if (filterName == nameof(printerResource.Printer) && value is long printerId)
        {
            printerResource.Printer = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId) ?? new();
        }
        if (filterName == nameof(printerResource.Resource) && value is long resourceId)
        {
            printerResource.Resource = AppSettings.DataAccess.GetItemById<TemplateResourceModel>(resourceId) ?? new();
        }
    }

    private void OnItemValueChangePlu(string? filterName, object? value, PluObsoleteModel pluObsolete)
    {
        if (filterName == nameof(pluObsolete.Nomenclature) && value is long nomenclatureId)
        {
            pluObsolete.Nomenclature = AppSettings.DataAccess.GetItemById<NomenclatureModel>(nomenclatureId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Scale) && value is long scaleId)
        {
            pluObsolete.Scale = AppSettings.DataAccess.GetItemById<ScaleModel>(scaleId) ?? new();
        }
        if (filterName == nameof(pluObsolete.Template) && value is long templateId)
        {
            pluObsolete.Template = AppSettings.DataAccess.GetItemById<TemplateModel>(templateId) ?? new();
        }
    }

    private void OnItemValueChangeScale(string? filterName, object? value, ScaleModel scale)
    {
        if (filterName == nameof(ScaleModel.Identity.Id) && value is long id)
        {
            scale = AppSettings.DataAccess.GetItemById<ScaleModel>(id) ?? new();
        }
        if (filterName == nameof(ScaleModel.DeviceComPort) && value is string deviceComPort)
        {
            scale.DeviceComPort = deviceComPort;
        }
        if (filterName == nameof(ScaleModel.Host) && value is long hostId)
        {
            scale.Host = AppSettings.DataAccess.GetItemById<HostModel>(hostId);
        }
        if (filterName == nameof(ScaleModel.TemplateDefault) && value is long templateDefaultId)
        {
            scale.TemplateDefault = AppSettings.DataAccess.GetItemById<TemplateModel>(templateDefaultId);
        }
        if (filterName == nameof(ScaleModel.TemplateSeries) && value is long templateSeriesId)
        {
            scale.TemplateSeries = AppSettings.DataAccess.GetItemById<TemplateModel>(templateSeriesId);
        }
        if (filterName == nameof(ScaleModel.PrinterMain) && value is long printerId)
        {
            scale.PrinterMain = AppSettings.DataAccess.GetItemById<PrinterModel>(printerId);
        }
        if (filterName == nameof(ScaleModel.WorkShop) && value is long workShopId)
        {
            scale.WorkShop = AppSettings.DataAccess.GetItemById<WorkShopModel>(workShopId);
        }
    }

    private void OnItemValueChangeTemplate(string? filterName, object? value, TemplateModel template)
    {
        if (filterName == nameof(template.CategoryId) && value is string categoryId)
        {
            template.CategoryId = categoryId;
        }
    }

    private void OnItemValueChangeWorkShop(string? filterName, object? value, WorkShopModel workshop)
    {
        if (filterName == nameof(workshop.ProductionFacility) && value is int productionFacilityId)
        {
            workshop.ProductionFacility = AppSettings.DataAccess.GetItemById<ProductionFacilityModel>(productionFacilityId) ?? new();
        }
    }

    #endregion

    #region Public and private methods - Actions

    protected async Task ItemCancelAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsSafe(LocaleCore.Dialog.DialogResultSuccess, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                SetRouteSectionNavigate(false);
            });
    }

    private string GetQuestionAdd()
    {
        switch (ParentRazor?.Item?.Identity.Name)
        {
            case SqlFieldIdentityEnum.Id:
                return LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
                       $"{nameof(ParentRazor.Item.Identity.Id)}: {ParentRazor.Item.Identity.Id}";
            case SqlFieldIdentityEnum.Uid:
                return LocaleCore.Dialog.DialogQuestion + Environment.NewLine +
                       $"{nameof(ParentRazor.Item.Identity.Uid)}: {ParentRazor.Item.Identity.Uid}";
        }
        return string.Empty;
    }

    private void ItemScaleSave(SqlTableScaleEnum tableScale)
    {
        switch (tableScale)
        {
	        case SqlTableScaleEnum.Accesses:
		        ItemSaveCheck.Access(NotificationService, (AccessModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
		        break;
	        case SqlTableScaleEnum.Tasks:
		        ItemSaveCheck.Task(NotificationService, (TaskModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
		        break;
	        case SqlTableScaleEnum.TasksTypes:
		        ItemSaveCheck.TaskType(NotificationService, (TaskTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
		        break;
			case SqlTableScaleEnum.BarCodesTypes:
                ItemSaveCheck.BarcodeType(NotificationService, (BarCodeTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Contragents:
                ItemSaveCheck.Contragent(NotificationService, (ContragentModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Hosts:
                ItemSaveCheck.Host(NotificationService, (HostModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Nomenclatures:
                ItemSaveCheck.Nomenclature(NotificationService, (NomenclatureModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.PlusObsolete:
                ItemSaveCheck.PluObsolete(NotificationService, (PluObsoleteModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Plus:
                ItemSaveCheck.Plu(NotificationService, (PluModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.PlusScales:
                if (ParentRazor?.Items is not null)
                {
                    List<PluScaleModel> pluScales = ParentRazor.Items.Cast<PluScaleModel>().ToList();
                    foreach (PluScaleModel pluScale in pluScales)
                    {
                        ItemSaveCheck.PluScale(NotificationService, pluScale, SqlTableActionEnum.Save);
                    }
                }
                else if (ParentRazor?.Item is not null)
                {
                    ItemSaveCheck.PluScale(NotificationService, (PluScaleModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                }
                break;
            case SqlTableScaleEnum.PrintersResources:
                ItemSaveCheck.PrinterResource(NotificationService, (PrinterResourceModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Printers:
                ItemSaveCheck.Printer(NotificationService, (PrinterModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.PrintersTypes:
                ItemSaveCheck.PrinterType(NotificationService, (PrinterTypeModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.ProductionFacilities:
                ItemSaveCheck.ProductionFacility(NotificationService, (ProductionFacilityModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Scales:
                ItemSaveCheck.Scale(NotificationService, Item, SqlTableActionEnum.Save);
                break;
            case SqlTableScaleEnum.Templates:
                ItemSaveCheck.Template(NotificationService, (TemplateModel?)ParentRazor?.Item, ParentRazor?.TableAction);
                break;
            case SqlTableScaleEnum.TemplatesResources:
                ItemSaveCheck.TemplateResource(NotificationService, (TemplateResourceModel?)ParentRazor?.Item, ParentRazor?.TableAction);
                break;
            case SqlTableScaleEnum.WorkShops:
                ItemSaveCheck.Workshop(NotificationService, (WorkShopModel?)ParentRazor?.Item, SqlTableActionEnum.Save);
                break;
        }
    }

    protected async Task ItemSaveAsync()
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        RunActionsWithQeustion(LocaleCore.Table.TableSave, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                ItemScaleSave(SqlUtils.GetTableScale(Table.Name));
                SetRouteSectionNavigate(false);
            });

        ParentRazor?.OnChange();
    }

    protected async Task ActionNewAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                throw new NotImplementedException("Fix here!");
                // Uncomment here.
                //item = new();
                //Identity.Id = null;
                //IdentityUid = null;
                //RouteItemNavigate(isNewWindow, item, DbTableAction.New);
            });
    }

    protected async Task ActionCopyAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                SetRouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, SqlTableActionEnum.Copy);
            });

        ParentRazor?.OnChange();
    }

    protected async Task ActionEditAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                SetRouteItemNavigate(isNewWindow, isParentRazor ? ParentRazor?.Item : Item, SqlTableActionEnum.Edit);
                InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionPluScalePlusClickAsync(UserSettingsModel? userSettings, PluScaleModel pluScale, List<PluScaleModel> pluScales)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        //foreach (PluScaleModel item in pluScales)
        //{
        // if (item.Identity.Uid.Equals(pluScale.Identity.Uid))
        // {
        //  item.IsActive = pluScale.IsActive;
        // }
        //}
    }

    public async Task ActionSaveAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsSafe(string.Empty, LocaleCore.Dialog.DialogResultFail,
            () =>
            {
                //AppSettings.DataAccess.Save(isParentRazor ? ParentRazor?.Item : Item);
                InvokeAsync(StateHasChanged);
            });
    }

    protected async Task ActionMarkAsync(UserSettingsModel? userSettings, bool isNewWindow, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Table.TableMark, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Mark(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task ActionDeleteAsync(UserSettingsModel? userSettings, bool isParentRazor)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Table.TableDelete, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                AppSettings.DataAccess.Delete(isParentRazor ? ParentRazor?.Item : Item);
            });

        ParentRazor?.OnChange();
    }

    protected async Task PrinterResourcesClear(UserSettingsModel? userSettings, PrinterModel printer)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesClear, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
                List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
                foreach (TemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains("TTF"))
                    {
                        TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                            new()
                            {
                                new($"^XA^ID"),
                                new(templateResource.Name),
                                new($"^FS^XZ")
                            });
                    }
                }
            });
    }

    protected async Task PrinterResourcesLoad(UserSettingsModel? userSettings, PrinterModel printer, string fileType)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

        if (userSettings is null || !userSettings.AccessRightsIsWrite)
            return;

        RunActionsWithQeustion(LocaleCore.Print.ResourcesLoadTtf, LocaleCore.Dialog.DialogResultSuccess,
            LocaleCore.Dialog.DialogResultFail, GetQuestionAdd(),
            () =>
            {
                SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
                List<TemplateResourceModel> templateResources = AppSettings.DataAccess.GetList<TemplateResourceModel>(sqlCrudConfig);
                foreach (TemplateResourceModel templateResource in templateResources)
                {
                    if (templateResource.Name.Contains(fileType))
                    {
                        TcpClient client = ZplUtils.TcpClientSendData(printer.Ip, printer.Port,
                            new()
                            {
                                new($"^XA^MNN^LL500~DYE:{templateResource.Name}.TTF,B,T,{templateResource.ImageData.Value.Length},,"),
                                new(templateResource.ImageData.Value),
                                new($"^XZ")
                            });
                    }
                }
            });
    }

    #endregion
}
