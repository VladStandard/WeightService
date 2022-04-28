// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using DataCore.Localizations;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemPrinterResource
    {
        #region Public and private fields and properties

        public PrinterResourceEntity ItemCast { get => Item == null ? new() : (PrinterResourceEntity)Item; set => Item = value; }
        public List<PrinterEntity>? PrinterItems { get; set; } = null;
        public List<TemplateResourceEntity>? ResourceItems { get; set; } = null;

        #endregion

        #region Constructor and destructor

        public ItemPrinterResource() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            IsLoaded = false;
            Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
            ItemCast = new();
            PrinterItems = null;
            ResourceItems = null;
            ButtonSettings = new();
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocaleCore.Action.ActionMethod} {nameof(SetParametersAsync)}", "", LocaleCore.Dialog.DialogResultFail, "",
                new Task(async() => {
                    Default();
                    await GuiRefreshWithWaitAsync();

                    switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                            ItemCast.Description = "NEW RESOURCE";
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<PrinterResourceEntity>(
                                new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IdentityId, IdentityId } }), null);
                            break;
                    }

                    PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                        new FieldListEntity(new Dictionary<DbField, object?> { { DbField.IsMarked, false } }),
                        null)?.ToList();
                    ResourceItems = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>(null, null)?.ToList();
                    ButtonSettings = new(false, false, false, false, false, true, true);
                    IsLoaded = true;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
