// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ItemDates
    {
        #region Public and private fields and properties

        [Parameter] public string CreateDt { get; set; } = string.Empty;
        [Parameter] public string ChangeDt { get; set; } = string.Empty;

        #endregion

        #region Constructor and destructor

        public ItemDates() : base()
        {
            //
        }

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
                    BarCodeTypeEntityV2? barcodeType = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeEntityV2>(IdentityUid);
                    if (barcodeType != null)
                    {
                        CreateDt = barcodeType.CreateDt.ToString();
                        ChangeDt = barcodeType.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    ContragentEntityV2? contragent = AppSettings.DataAccess.Crud.GetEntity<ContragentEntityV2>(IdentityUid);
                    if (contragent != null)
                    {
                        CreateDt = contragent.CreateDt.ToString();
                        ChangeDt = contragent.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    HostEntity? host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(IdentityId);
                    if (host != null)
                    {
                        CreateDt = host.CreateDt.ToString();
                        ChangeDt = host.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Labels:
                    LabelEntity? label = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(IdentityId);
                    if (label != null)
                    {
                        CreateDt = label.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    NomenclatureEntity? nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(IdentityId);
                    if (nomenclature != null)
                    {
                        CreateDt = nomenclature.CreateDt.ToString();
                        ChangeDt = nomenclature.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Orders:
                    OrderEntity? order = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(IdentityId);
                    if (order != null)
                    {
                        CreateDt = order.CreateDt.ToString();
                        ChangeDt = order.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersStatuses:
                    OrderStatusEntity? orderStatus = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>(IdentityId);
                    if (orderStatus != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersTypes:
                    OrderTypeEntity? orderType = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>(IdentityId);
                    if (orderType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.Organizations:
                    OrganizationEntity? organization = AppSettings.DataAccess.Crud.GetEntity<OrganizationEntity>(IdentityId);
                    if (organization != null)
                    {
                        CreateDt = organization.CreateDt.ToString();
                        ChangeDt = organization.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Plus:
                    PluEntity? plu = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(IdentityId);
                    if (plu != null)
                    {
                        CreateDt = plu.CreateDt.ToString();
                        ChangeDt = plu.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Printers:
                    PrinterEntity? printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(IdentityId);
                    if (printer != null)
                    {
                        CreateDt = printer.CreateDt.ToString();
                        ChangeDt = printer.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersResources:
                    PrinterResourceEntity? printerResource = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(IdentityId);
                    if (printerResource != null)
                    {
                        CreateDt = printerResource.CreateDt.ToString();
                        ChangeDt = printerResource.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersTypes:
                    PrinterTypeEntity? printerType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(IdentityId);
                    if (printerType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    ProductionFacilityEntity? productionFacility = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(IdentityId);
                    if (productionFacility != null)
                    {
                        CreateDt = productionFacility.CreateDt.ToString();
                        ChangeDt = productionFacility.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    ProductSeriesEntity? productSeries = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(IdentityId);
                    if (productSeries != null)
                    {
                        CreateDt = productSeries.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Scales:
                    ScaleEntity? scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(IdentityId);
                    if (scale != null)
                    {
                        CreateDt = scale.CreateDt.ToString();
                        ChangeDt = scale.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Templates:
                    TemplateEntity? template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(IdentityId);
                    if (template != null)
                    {
                        CreateDt = template.CreateDt.ToString();
                        ChangeDt = template.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.TemplatesResources:
                    TemplateResourceEntity? templateResource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(IdentityId);
                    if (templateResource != null)
                    {
                        CreateDt = templateResource.CreateDt.ToString();
                        ChangeDt = templateResource.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    WeithingFactEntity? weithingFact = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>(IdentityId);
                    if (weithingFact != null)
                    {
                        CreateDt = weithingFact.CreateDt.ToString();
                        ChangeDt = weithingFact.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    WorkShopEntity? workshop = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(IdentityId);
                    if (workshop != null)
                    {
                        CreateDt = workshop.CreateDt.ToString();
                        ChangeDt = workshop.ChangeDt.ToString();
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
                    AccessEntity? access = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(IdentityId);
                    if (access != null)
                    {
                        CreateDt = access.CreateDt.ToString();
                        ChangeDt = access.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    ErrorEntity? error = AppSettings.DataAccess.Crud.GetEntity<ErrorEntity>(IdentityId);
                    if (error != null)
                    {
                        CreateDt = error.CreateDt.ToString();
                        ChangeDt = error.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    LogEntity? log = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(IdentityUid);
                    if (log != null)
                    {
                        CreateDt = log.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.LogTypes:
                    LogTypeEntity? logType = AppSettings.DataAccess.Crud.GetEntity<LogTypeEntity>(IdentityId);
                    if (logType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableSystem.Tasks:
                    TaskEntity? task = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>(IdentityId);
                    if (task != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableSystem.TasksTypes:
                    TaskTypeEntity? taskType = AppSettings.DataAccess.Crud.GetEntity<TaskTypeEntity>(IdentityId);
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
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.Sql.TableDwhModels.InformationSystemEntity>(IdentityId);
                    if (informationSystem != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.Nomenclature:
                    DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclature =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                    if (nomenclature != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.NomenclatureMaster:
                    DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclatureMaster =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                    if (nomenclatureMaster != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.NomenclatureNonNormalize:
                    DataCore.Sql.TableDwhModels.NomenclatureEntity? nomenclatureNonNormilize =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.Sql.TableDwhModels.NomenclatureEntity>(IdentityId);
                    if (nomenclatureNonNormilize != null)
                    {
                    }
                    break;
            }
        }

        #endregion
    }
}
