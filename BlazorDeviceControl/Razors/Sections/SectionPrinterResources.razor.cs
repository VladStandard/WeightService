// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPrinterResources : BlazorCore.Models.RazorBase
{
    #region Public and private fields, properties, constructor

    private List<PrinterResourceEntity> ItemsCast
    {
        get => Items == null ? new() : Items.Select(x => (PrinterResourceEntity)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersResources);
        ItemsCast = new();
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
                List<FieldFilterModel> filters = IsShowMarkedFilter ? new() : new List<FieldFilterModel> { new(DbField.IsMarked, DbComparer.Equal, false) };
                if (printerId is not null)
                    filters.Add(new($"{nameof(PrinterResourceEntity.Printer)}.{DbField.IdentityId}", DbComparer.Equal, printerId));
                ItemsCast = AppSettings.DataAccess.Crud.GetItemsListNotNull<PrinterResourceEntity>(IsShowOnlyTop, filters, new(DbField.Description));

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
