// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
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

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(() => {
                    lock (Locker)
                    {
                        switch (Table)
                        {
                            case TableSystemEntity:
                                switch (ProjectsEnums.GetTableSystem(Table.Name))
                                {
                                    case ProjectsEnums.TableSystem.Default:
                                        break;
                                    case ProjectsEnums.TableSystem.Accesses:
                                        AccessEntity access = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>((int)Id);
                                        DtCreate = access.CreateDt.ToString();
                                        DtModify = access.ChangeDt.ToString();
                                        break;
                                    case ProjectsEnums.TableSystem.Errors:
                                        ErrorEntity error = AppSettings.DataAccess.Crud.GetEntity<ErrorEntity>((int)Id);
                                        DtCreate = error.CreatedDate.ToString();
                                        DtModify = error.ModifiedDate.ToString();
                                        break;
                                    case ProjectsEnums.TableSystem.Logs:
                                        LogEntity log = AppSettings.DataAccess.Crud.GetEntity<LogEntity>((Guid)Uid);
                                        DtCreate = log.CreateDt.ToString();
                                        break;
                                    case ProjectsEnums.TableSystem.LogTypes:
                                        break;
                                    case ProjectsEnums.TableSystem.Tasks:
                                        break;
                                    case ProjectsEnums.TableSystem.TasksTypes:
                                        break;
                                }
                                break;
                            case TableScaleEntity:
                                {
                                    switch (ProjectsEnums.GetTableScale(Table.Name))
                                    {
                                        case ProjectsEnums.TableScale.Default:
                                            break;
                                        case ProjectsEnums.TableScale.BarcodesTypes:
                                            break;
                                        case ProjectsEnums.TableScale.Contragents:
                                            break;
                                        case ProjectsEnums.TableScale.Hosts:
                                            HostEntity host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>((int)Id);
                                            DtCreate = host.CreateDate.ToString();
                                            DtModify = host.ModifiedDate.ToString();
                                            break;
                                        case ProjectsEnums.TableScale.Labels:
                                            break;
                                        case ProjectsEnums.TableScale.Nomenclatures:
                                            break;
                                        case ProjectsEnums.TableScale.Orders:
                                            break;
                                        case ProjectsEnums.TableScale.OrdersStatuses:
                                            break;
                                        case ProjectsEnums.TableScale.OrdersTypes:
                                            break;
                                        case ProjectsEnums.TableScale.Organizations:
                                            break;
                                        case ProjectsEnums.TableScale.Plus:
                                            break;
                                        case ProjectsEnums.TableScale.Printers:
                                            PrinterEntity printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>((int)Id);
                                            DtCreate = printer.CreateDate.ToString();
                                            DtModify = printer.ModifiedDate.ToString();
                                            break;
                                        case ProjectsEnums.TableScale.PrintersResources:
                                            break;
                                        case ProjectsEnums.TableScale.PrintersTypes:
                                            break;
                                        case ProjectsEnums.TableScale.ProductionFacilities:
                                            break;
                                        case ProjectsEnums.TableScale.ProductSeries:
                                            break;
                                        case ProjectsEnums.TableScale.Scales:
                                            ScaleEntity scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>((int)Id);
                                            DtCreate = scale.CreateDate.ToString();
                                            DtModify = scale.ModifiedDate.ToString();
                                            break;
                                        case ProjectsEnums.TableScale.Templates:
                                            break;
                                        case ProjectsEnums.TableScale.TemplatesResources:
                                            break;
                                        case ProjectsEnums.TableScale.WeithingFacts:
                                            break;
                                        case ProjectsEnums.TableScale.Workshops:
                                            break;
                                    }

                                    break;
                                }
                            case TableDwhEntity:
                                break;
                        }
                    }
                }), true);
        }

        #endregion
    }
}
