// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionPrinterResources
{
    #region Public and private fields, properties, constructor

    private List<PrinterResourceEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PrinterResourceEntity)x).ToList();

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
        Items = new();
	}

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        RunActions(new()
        {
            () =>
            {
                long? printerId = null;
                if (ItemFilter is PrinterEntity printer)
                    printerId = printer.IdentityId;
                if (IsShowMarkedItems)
                {
                    if (printerId == null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                            null,
                            new(DbField.Description, DbOrderDirection.Asc),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                        ?.ToList<BaseEntity>();
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                            new(new() { new($"Printer.{DbField.IdentityId}", DbComparer.Equal, printerId) }),
                            new(DbField.Description, DbOrderDirection.Asc),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }
                else
                {
                    if (printerId == null)
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(
                            new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                            new(DbField.Description, DbOrderDirection.Asc),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    else
                    {
                        Items = AppSettings.DataAccess.Crud.GetEntities<PrinterResourceEntity>(new(
                                new() { new($"Printer.{DbField.IdentityId}", DbComparer.Equal, printerId),
                                new(DbField.IsMarked, DbComparer.Equal, false)
                            }),
                            new(DbField.Description, DbOrderDirection.Asc),
                            IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                            ?.ToList<BaseEntity>();
                    }
                }
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
