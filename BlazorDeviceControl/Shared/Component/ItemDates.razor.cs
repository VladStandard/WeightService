// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Globalization;
using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component;

public partial class ItemDates : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    [Parameter] public string CreateDt { get; set; } = string.Empty;
    [Parameter] public string ChangeDt { get; set; } = string.Empty;

    #endregion

    #region Public and private methods

    private void Default()
    {
        IsLoaded = false;
        switch (Table)
        {
            case TableSystemEntity:
                SetDtFromTableSystem();
                break;
            case TableScaleEntity:
                SetDtFromTableScale();
                break;
            case TableDwhEntity:
                SetDtFromTableDwh();
                break;
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters).ConfigureAwait(true);
        RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
            new Task(async () =>
            {
                Default();
                IsLoaded = true;
                await GuiRefreshWithWaitAsync();
            }), true);
    }

    private void SetDtFromTableScale()
    {
        switch (ProjectsEnums.GetTableScale(Table.Name))
        {
            case ProjectsEnums.TableScale.Default:
                break;
            case ProjectsEnums.TableScale.BarCodeTypes:
                BarCodeTypeV2Entity? barcodeType = AppSettings.DataAccess.Crud.GetEntityByUid<BarCodeTypeV2Entity>(IdentityUid);
                if (barcodeType != null)
                {
                    CreateDt = barcodeType.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = barcodeType.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Contragents:
                ContragentV2Entity? contragent = AppSettings.DataAccess.Crud.GetEntityByUid<ContragentV2Entity>(IdentityUid);
                if (contragent != null)
                {
                    CreateDt = contragent.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = contragent.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Hosts:
                HostEntity? host = AppSettings.DataAccess.Crud.GetEntityById<HostEntity>(IdentityId);
                if (host != null)
                {
                    CreateDt = host.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = host.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Labels:
                LabelEntity? label = AppSettings.DataAccess.Crud.GetEntityById<LabelEntity>(IdentityId);
                if (label != null)
                {
                    CreateDt = label.CreateDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Nomenclatures:
                NomenclatureEntity? nomenclature = AppSettings.DataAccess.Crud.GetEntityById<NomenclatureEntity>(IdentityId);
                if (nomenclature != null)
                {
                    CreateDt = nomenclature.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = nomenclature.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Orders:
                OrderEntity? order = AppSettings.DataAccess.Crud.GetEntityById<OrderEntity>(IdentityId);
                if (order != null)
                {
                    CreateDt = order.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = order.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.OrdersStatuses:
                OrderStatusEntity? orderStatus = AppSettings.DataAccess.Crud.GetEntityById<OrderStatusEntity>(IdentityId);
                if (orderStatus != null)
                {
                }
                break;
            case ProjectsEnums.TableScale.OrdersTypes:
                OrderTypeEntity? orderType = AppSettings.DataAccess.Crud.GetEntityById<OrderTypeEntity>(IdentityId);
                if (orderType != null)
                {
                }
                break;
            case ProjectsEnums.TableScale.Organizations:
                OrganizationEntity? organization = AppSettings.DataAccess.Crud.GetEntityById<OrganizationEntity>(IdentityId);
                if (organization != null)
                {
                    CreateDt = organization.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = organization.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Plus:
                PluEntity? plu = AppSettings.DataAccess.Crud.GetEntityById<PluEntity>(IdentityId);
                if (plu != null)
                {
                    CreateDt = plu.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = plu.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Printers:
                PrinterEntity? printer = AppSettings.DataAccess.Crud.GetEntityById<PrinterEntity>(IdentityId);
                if (printer != null)
                {
                    CreateDt = printer.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = printer.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.PrintersResources:
                PrinterResourceEntity? printerResource = AppSettings.DataAccess.Crud.GetEntityById<PrinterResourceEntity>(IdentityId);
                if (printerResource != null)
                {
                    CreateDt = printerResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = printerResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.PrintersTypes:
                PrinterTypeEntity? printerType = AppSettings.DataAccess.Crud.GetEntityById<PrinterTypeEntity>(IdentityId);
                if (printerType != null)
                {
                }
                break;
            case ProjectsEnums.TableScale.ProductionFacilities:
                ProductionFacilityEntity? productionFacility = AppSettings.DataAccess.Crud.GetEntityById<ProductionFacilityEntity>(IdentityId);
                if (productionFacility != null)
                {
                    CreateDt = productionFacility.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = productionFacility.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.ProductSeries:
                ProductSeriesEntity? productSeries = AppSettings.DataAccess.Crud.GetEntityById<ProductSeriesEntity>(IdentityId);
                if (productSeries != null)
                {
                    CreateDt = productSeries.CreateDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Scales:
                ScaleEntity? scale = AppSettings.DataAccess.Crud.GetEntityById<ScaleEntity>(IdentityId);
                if (scale != null)
                {
                    CreateDt = scale.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = scale.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Templates:
                TemplateEntity? template = AppSettings.DataAccess.Crud.GetEntityById<TemplateEntity>(IdentityId);
                if (template != null)
                {
                    CreateDt = template.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = template.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.TemplatesResources:
                TemplateResourceEntity? templateResource = AppSettings.DataAccess.Crud.GetEntityById<TemplateResourceEntity>(IdentityId);
                if (templateResource != null)
                {
                    CreateDt = templateResource.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = templateResource.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.WeithingFacts:
                WeithingFactEntity? weithingFact = AppSettings.DataAccess.Crud.GetEntityById<WeithingFactEntity>(IdentityId);
                if (weithingFact != null)
                {
                    CreateDt = weithingFact.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = weithingFact.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.Workshops:
                WorkShopEntity? workshop = AppSettings.DataAccess.Crud.GetEntityById<WorkShopEntity>(IdentityId);
                if (workshop != null)
                {
                    CreateDt = workshop.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = workshop.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableScale.BarCodes:
                break;
        }
    }

    private void SetDtFromTableSystem()
    {
        switch (ProjectsEnums.GetTableSystem(Table.Name))
        {
            case ProjectsEnums.TableSystem.Default:
                break;
            case ProjectsEnums.TableSystem.Accesses:
                AccessEntity? access = AppSettings.DataAccess.Crud.GetEntityById<AccessEntity>(IdentityId);
                if (access != null)
                {
                    CreateDt = access.CreateDt.ToString(CultureInfo.InvariantCulture);
                    ChangeDt = access.ChangeDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableSystem.Logs:
                LogEntity? log = AppSettings.DataAccess.Crud.GetEntityByUid<LogEntity>(IdentityUid);
                if (log != null)
                {
                    CreateDt = log.CreateDt.ToString(CultureInfo.InvariantCulture);
                }
                break;
            case ProjectsEnums.TableSystem.LogTypes:
                LogTypeEntity? logType = AppSettings.DataAccess.Crud.GetEntityById<LogTypeEntity>(IdentityId);
                if (logType != null)
                {
                }
                break;
            case ProjectsEnums.TableSystem.Tasks:
                TaskEntity? task = AppSettings.DataAccess.Crud.GetEntityById<TaskEntity>(IdentityId);
                if (task != null)
                {
                }
                break;
            case ProjectsEnums.TableSystem.TasksTypes:
                TaskTypeEntity? taskType = AppSettings.DataAccess.Crud.GetEntityById<TaskTypeEntity>(IdentityId);
                if (taskType != null)
                {
                }
                break;
        }
    }

    private void SetDtFromTableDwh()
    {
        switch (ProjectsEnums.GetTableDwh(Table.Name))
        {
            case ProjectsEnums.TableDwh.Default:
                break;
            case ProjectsEnums.TableDwh.InformationSystem:
                DataCore.Sql.TableDwhModels.InformationSystemEntity? informationSystem =
                    AppSettings.DataAccess.Crud.GetEntityById<DataCore.Sql.TableDwhModels.InformationSystemEntity>(IdentityId);
                if (informationSystem != null)
                {
                }
                break;
            case ProjectsEnums.TableDwh.Nomenclature:
                DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclature =
                    AppSettings.DataAccess.Crud.GetEntityById<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                if (nomenclature != null)
                {
                }
                break;
            case ProjectsEnums.TableDwh.NomenclatureMaster:
                DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclatureMaster =
                    AppSettings.DataAccess.Crud.GetEntityById<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                if (nomenclatureMaster != null)
                {
                }
                break;
            case ProjectsEnums.TableDwh.NomenclatureNonNormalize:
                DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclatureNonNormilize =
                    AppSettings.DataAccess.Crud.GetEntityById<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                if (nomenclatureNonNormilize != null)
                {
                }
                break;
        }
    }

    #endregion
}
