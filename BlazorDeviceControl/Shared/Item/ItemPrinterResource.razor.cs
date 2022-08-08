// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

/// <summary>
/// Item PrinterResource page.
/// </summary>
public partial class ItemPrinterResource
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// PrinterResource.
    /// </summary>
    private PrinterResourceEntity ItemCast { get => Item == null ? new() : (PrinterResourceEntity)Item; set => Item = value; }
    /// <summary>
    /// Printers.
    /// </summary>
    private List<PrinterEntity>? PrinterItems { get; set; }
    /// <summary>
    /// Printer's resources.
    /// </summary>
    private List<TemplateResourceEntity>? ResourceItems { get; set; }

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
            new Task(async () =>
            {
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
                            new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
                        break;
                }

                PrinterItems = AppSettings.DataAccess.Crud.GetEntities<PrinterEntity>(
                    new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Name))?.ToList();
                ResourceItems = AppSettings.DataAccess.Crud.GetEntities<TemplateResourceEntity>()?.ToList();
                ButtonSettings = new(false, false, false, false, false, true, true);
                IsLoaded = true;
                await GuiRefreshWithWaitAsync();
            }), true);
    }

    #endregion
}
