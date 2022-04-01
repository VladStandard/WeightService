// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemPrinter
    {
        #region Public and private fields and properties

        public PrinterEntity? ItemCast { get => Item == null ? null : (PrinterEntity)Item; set => Item = value; }
        public List<PrinterTypeEntity>? PrinterTypes { get; set; } = null;
        private readonly object _locker = new();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    lock (_locker)
                    {
                        Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
                        ItemCast = null;
                        PrinterTypes = null;
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        switch (TableAction)
                        {
                            case DbTableAction.New:
                                ItemCast = new();
                                ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                                ItemCast.IsMarked = false;
                                ItemCast.Name = "NEW PRINTER";
                                break;
                            default:
                                ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterEntity>(
                                    new FieldListEntity(new Dictionary<string, object?> 
                                    { { DbField.Id.ToString(), Id } }), null);
                                break;
                        }

                        PrinterTypes = AppSettings.DataAccess.Crud.GetEntities<PrinterTypeEntity>(null, null)?.ToList();
                        if (Id != null && TableAction == DbTableAction.New)
                        {
                            ItemCast.Id = (int)Id;
                            ItemCast.Name = "NEW PRINTER";
                            ItemCast.Ip = "127.0.0.1";
                            ItemCast.MacAddress.Default();
                        }
                        ButtonSettings = new(false, false, false, false, false, true, true);
                    }

                    await ItemCast.SetHttpStatusAsync().ConfigureAwait(true);

                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
