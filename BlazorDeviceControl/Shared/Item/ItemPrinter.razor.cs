// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemPrinter
    {
		#region Public and private fields, properties, constructor

		private PrinterEntity ItemCast { get => Item == null ? new() : (PrinterEntity)Item; set => Item = value; }
		private List<PrinterTypeEntity>? PrinterTypes { get; set; }

        #endregion

        #region Constructor and destructor

        public ItemPrinter()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.Printers);
            ItemCast = new();
            PrinterTypes = null;
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();

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
                                new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
                            break;
                    }

                    PrinterTypes = AppSettings.DataAccess.Crud.GetEntities<PrinterTypeEntity>(null, null)?.ToList();
                    if (IdentityId != null && TableAction == DbTableAction.New)
                    {
                        ItemCast.IdentityId = (long)IdentityId;
                        ItemCast.Name = "NEW PRINTER";
                        ItemCast.Ip = "127.0.0.1";
                        ItemCast.MacAddress.Default();
                    }
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    //await ItemCast.SetHttpStatusAsync().ConfigureAwait(true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
