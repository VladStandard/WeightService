// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore;
using DataCore.DAL.TableScaleModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ItemDates
    {
        #region Public and private fields and properties

        [Parameter] public string DtCreate { get; set; }
        [Parameter] public string DtModify { get; set; }
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () => {
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
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        private void SetDtFromTableScale()
        {
            switch (ProjectsEnums.GetTableScale(Table.Name))
            {
                case ProjectsEnums.TableScale.Default:
                    break;
                case ProjectsEnums.TableScale.BarcodesTypes:
                    BarcodeTypeEntity barcodeType = AppSettings.DataAccess.Crud.GetEntity<BarcodeTypeEntity>((long)Id);
                    break;
                case ProjectsEnums.TableScale.Contragents:
                    ContragentEntity contragent = AppSettings.DataAccess.Crud.GetEntity<ContragentEntity>((long)Id);
                    DtCreate = contragent.CreateDate.ToString();
                    DtModify = contragent.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Hosts:
                    HostEntity host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>((long)Id);
                    DtCreate = host.CreateDt.ToString();
                    DtModify = host.ChangeDt.ToString();
                    break;
                case ProjectsEnums.TableScale.Labels:
                    LabelEntity label = AppSettings.DataAccess.Crud.GetEntity<LabelEntity>((long)Id);
                    DtCreate = label.CreateDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Nomenclatures:
                    NomenclatureEntity nomenclature = AppSettings.DataAccess.Crud.GetEntity<NomenclatureEntity>((long)Id);
                    DtCreate = nomenclature.CreateDate.ToString();
                    DtModify = nomenclature.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Orders:
                    OrderEntity order = AppSettings.DataAccess.Crud.GetEntity<OrderEntity>((long)Id);
                    DtCreate = order.CreateDate.ToString();
                    DtModify = order.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.OrdersStatuses:
                    OrderStatusEntity orderStatus = AppSettings.DataAccess.Crud.GetEntity<OrderStatusEntity>((long)Id);
                    break;
                case ProjectsEnums.TableScale.OrdersTypes:
                    OrderTypeEntity orderType = AppSettings.DataAccess.Crud.GetEntity<OrderTypeEntity>((long)Id);
                    break;
                case ProjectsEnums.TableScale.Organizations:
                    OrganizationEntity organization = AppSettings.DataAccess.Crud.GetEntity<OrganizationEntity>((long)Id);
                    DtCreate = organization.CreateDate.ToString();
                    DtModify = organization.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Plus:
                    PluEntity plu = AppSettings.DataAccess.Crud.GetEntity<PluEntity>((long)Id);
                    DtCreate = plu.CreateDate.ToString();
                    DtModify = plu.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Printers:
                    PrinterEntity printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>((long)Id);
                    DtCreate = printer.CreateDate.ToString();
                    DtModify = printer.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.PrintersResources:
                    PrinterResourceEntity printerResource = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>((long)Id);
                    DtCreate = printerResource.CreateDate.ToString();
                    DtModify = printerResource.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.PrintersTypes:
                    PrinterTypeEntity printerType = AppSettings.DataAccess.Crud.GetEntity<PrinterTypeEntity>((long)Id);
                    break;
                case ProjectsEnums.TableScale.ProductionFacilities:
                    ProductionFacilityEntity productionFacility = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>((long)Id);
                    DtCreate = productionFacility.CreateDate.ToString();
                    DtModify = productionFacility.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.ProductSeries:
                    ProductSeriesEntity productSeries = AppSettings.DataAccess.Crud.GetEntity<ProductSeriesEntity>((long)Id);
                    DtCreate = productSeries.CreateDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Scales:
                    ScaleEntity scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>((long)Id);
                    DtCreate = scale.CreateDate.ToString();
                    DtModify = scale.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.Templates:
                    TemplateEntity template = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>((long)Id);
                    DtCreate = template.CreateDate.ToString();
                    DtModify = template.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.TemplatesResources:
                    TemplateResourceEntity templateResource = AppSettings.DataAccess.Crud.GetEntity<TemplateResourceEntity>((long)Id);
                    DtCreate = templateResource.CreateDate.ToString();
                    DtModify = templateResource.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableScale.WeithingFacts:
                    WeithingFactEntity weithingFact = AppSettings.DataAccess.Crud.GetEntity<WeithingFactEntity>((long)Id);
                    break;
                case ProjectsEnums.TableScale.Workshops:
                    WorkshopEntity workshop = AppSettings.DataAccess.Crud.GetEntity<WorkshopEntity>((long)Id);
                    DtCreate = workshop.CreateDate.ToString();
                    DtModify = workshop.ModifiedDate.ToString();
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
                    AccessEntity access = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>((long)Id);
                    DtCreate = access.CreateDt.ToString();
                    DtModify = access.ChangeDt.ToString();
                    break;
                case ProjectsEnums.TableSystem.Errors:
                    ErrorEntity error = AppSettings.DataAccess.Crud.GetEntity<ErrorEntity>((long)Id);
                    DtCreate = error.CreatedDate.ToString();
                    DtModify = error.ModifiedDate.ToString();
                    break;
                case ProjectsEnums.TableSystem.Logs:
                    LogEntity log = AppSettings.DataAccess.Crud.GetEntity<LogEntity>((Guid)Uid);
                    DtCreate = log.CreateDt.ToString();
                    break;
                case ProjectsEnums.TableSystem.LogTypes:
                    LogTypeEntity logType = AppSettings.DataAccess.Crud.GetEntity<LogTypeEntity>((long)Id);
                    break;
                case ProjectsEnums.TableSystem.Tasks:
                    TaskEntity task = AppSettings.DataAccess.Crud.GetEntity<TaskEntity>((long)Id);
                    break;
                case ProjectsEnums.TableSystem.TasksTypes:
                    TaskTypeEntity taskType = AppSettings.DataAccess.Crud.GetEntity<TaskTypeEntity>((long)Id);
                    break;
            }
        }

        private void SetDtFromTableDwh()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
