// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL.TableScaleModels;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using Microsoft.AspNetCore.Components;
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
                        if (Table is TableSystemEntity)
                        {
                            //
                        }
                        else if (Table is TableScaleEntity)
                        {
                            switch (ProjectsEnums.GetTableScale(Table.Name))
                            {
                                case ProjectsEnums.TableScale.Hosts:
                                    HostEntity host = AppSettings.DataAccess.Crud.GetEntity<HostEntity>((int)Id);
                                    DtCreate = host.CreateDate.ToString();
                                    DtModify = host.ModifiedDate.ToString();
                                    break;
                                case ProjectsEnums.TableScale.Scales:
                                    ScaleEntity scale = AppSettings.DataAccess.Crud.GetEntity<ScaleEntity>((int)Id);
                                    DtCreate = scale.CreateDate.ToString();
                                    DtModify = scale.ModifiedDate.ToString();
                                    break;
                                case ProjectsEnums.TableScale.Printers:
                                    PrinterEntity printer = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>((int)Id);
                                    DtCreate = printer.CreateDate.ToString();
                                    DtModify = printer.ModifiedDate.ToString();
                                    break;
                            }
                        }
                        else if (Table is TableDwhEntity)
                        {
                            //
                        }
                    }
                    //await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
