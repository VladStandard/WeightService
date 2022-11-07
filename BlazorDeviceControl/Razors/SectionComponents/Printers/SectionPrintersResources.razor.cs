// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Printers;

public partial class SectionPrintersResources : RazorComponentSectionBase<PrinterResourceModel, SqlTableBase>
{
	#region Public and private fields, properties, constructor

	public SectionPrintersResources()
    {
		SqlCrudConfigList.IsGuiShowItemsCount = true;
	    SqlCrudConfigList.IsGuiShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlCrudConfigList.SetFilters(nameof(PrinterResourceModel.Printer), ParentRazor ?.SqlItem, EnumFilterAction.Add);
				SqlItemsCast = DataContext.GetListNotNull<PrinterResourceModel>(SqlCrudConfigList);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
