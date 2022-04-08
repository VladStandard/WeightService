// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.TableScaleModels;
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
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public ItemDates()
        {
            Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            lock (_locker)
            {
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
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
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
                    BarCodeTypeEntityV2? barcodeType = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeEntityV2>(Uid);
                    if (barcodeType != null)
                    {
                        CreateDt = barcodeType.CreateDt.ToString();
                        ChangeDt = barcodeType.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    ContragentEntityV2? contragent = AppSettings.DataAccess.Crud.GetEntity<ContragentEntityV2>(Uid);
                    if (contragent != null)
                    {
                        CreateDt = contragent.CreateDt.ToString();
                        ChangeDt = contragent.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    HostEntity? host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(Id);
                    if (host != null)
                    {
                        CreateDt = host.CreateDt.ToString();
                        ChangeDt = host.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Labels:
                    LabelEntity? label = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>(Id);
                    if (label != null)
                    {
                        CreateDt = label.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    NomenclatureEntity? nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>(Id);
                    if (nomenclature != null)
                    {
                        CreateDt = nomenclature.CreateDt.ToString();
                        ChangeDt = nomenclature.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Orders:
                    OrderEntity? order = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>(Id);
                    if (order != null)
                    {
                        CreateDt = order.CreateDt.ToString();
                        ChangeDt = order.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersStatuses:
                    OrderStatusEntity? orderStatus = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>(Id);
                    if (orderStatus != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.OrdersTypes:
                    OrderTypeEntity? orderType = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>(Id);
                    if (orderType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.Organizations:
                    OrganizationEntity? organization = AppSettings.DataAccess.Crud.GetEntity<OrganizationEntity>(Id);
                    if (organization != null)
                    {
                        CreateDt = organization.CreateDt.ToString();
                        ChangeDt = organization.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Plus:
                    PluEntity? plu = AppSettings.DataAccess.Crud.GetEntity<PluEntity>(Id);
                    if (plu != null)
                    {
                        CreateDt = plu.CreateDt.ToString();
                        ChangeDt = plu.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Printers:
                    PrinterEntity? printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(Id);
                    if (printer != null)
                    {
                        CreateDt = printer.CreateDt.ToString();
                        ChangeDt = printer.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersResources:
                    PrinterResourceEntity? printerResource = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(Id);
                    if (printerResource != null)
                    {
                        CreateDt = printerResource.CreateDt.ToString();
                        ChangeDt = printerResource.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.PrintersTypes:
                    PrinterTypeEntity? printerType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>(Id);
                    if (printerType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    ProductionFacilityEntity? productionFacility = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(Id);
                    if (productionFacility != null)
                    {
                        CreateDt = productionFacility.CreateDt.ToString();
                        ChangeDt = productionFacility.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    ProductSeriesEntity? productSeries = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>(Id);
                    if (productSeries != null)
                    {
                        CreateDt = productSeries.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Scales:
                    ScaleEntity? scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>(Id);
                    if (scale != null)
                    {
                        CreateDt = scale.CreateDt.ToString();
                        ChangeDt = scale.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.Templates:
                    TemplateEntity? template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(Id);
                    if (template != null)
                    {
                        CreateDt = template.CreateDt.ToString();
                        ChangeDt = template.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.TemplatesResources:
                    TemplateResourceEntity? templateResource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>(Id);
                    if (templateResource != null)
                    {
                        CreateDt = templateResource.CreateDt.ToString();
                        ChangeDt = templateResource.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    WeithingFactEntity? weithingFact = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>(Id);
                    if (weithingFact != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    WorkShopEntity? workshop = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(Id);
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
                    AccessEntity? access = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(Id);
                    if (access != null)
                    {
                        CreateDt = access.CreateDt.ToString();
                        ChangeDt = access.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    ErrorEntity? error = AppSettings.DataAccess.Crud.GetEntity<ErrorEntity>(Id);
                    if (error != null)
                    {
                        CreateDt = error.CreateDt.ToString();
                        ChangeDt = error.ChangeDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    LogEntity? log = AppSettings.DataAccess.Crud.GetEntity<LogEntity>(Uid);
                    if (log != null)
                    {
                        CreateDt = log.CreateDt.ToString();
                    }
                    break;
                case ProjectsEnums.TableSystem.LogTypes:
                    LogTypeEntity? logType = AppSettings.DataAccess.Crud.GetEntity<LogTypeEntity>(Id);
                    if (logType != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableSystem.Tasks:
                    TaskEntity? task = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>(Id);
                    if (task != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableSystem.TasksTypes:
                    TaskTypeEntity? taskType = AppSettings.DataAccess.Crud.GetEntity<TaskTypeEntity>(Id);
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
                    DataCore.DAL.TableDwhModels.InformationSystemEntity? informationSystem = 
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.DAL.TableDwhModels.InformationSystemEntity>(Id);
                    if (informationSystem != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.Nomenclature:
                    DataCore.DAL.TableDwhModels.NomenclatureEntity? nomenclature =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.DAL.TableDwhModels.NomenclatureEntity>(Id);
                    if (nomenclature != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.NomenclatureMaster:
                    DataCore.DAL.TableDwhModels.NomenclatureEntity? nomenclatureMaster =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.DAL.TableDwhModels.NomenclatureEntity>(Id);
                    if (nomenclatureMaster != null)
                    {
                    }
                    break;
                case ProjectsEnums.TableDwh.NomenclatureNonNormalize:
                    DataCore.DAL.TableDwhModels.NomenclatureEntity? nomenclatureNonNormilize =
                        AppSettings.DataAccess.Crud.GetEntity<DataCore.DAL.TableDwhModels.NomenclatureEntity>(Id);
                    if (nomenclatureNonNormilize != null)
                    {
                    }
                    break;
            }
        }

        #endregion
    }
}
