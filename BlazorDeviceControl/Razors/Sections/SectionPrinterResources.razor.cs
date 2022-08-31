// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPrinterResources : RazorBase
{
    #region Public and private fields, properties, constructor

    private List<PrinterResourceModel> ItemsCast
    {
        get => Items == null ? new() : Items.Select(x => (PrinterResourceModel)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        Table = new TableScaleModel(ProjectsEnums.TableScale.PrintersResources);
        IsShowMarkedFilter = true;
		ItemsCast = new();
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActions(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.Crud.GetListPrinterResources(IsShowMarked, IsShowOnlyTop, ItemFilter);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
