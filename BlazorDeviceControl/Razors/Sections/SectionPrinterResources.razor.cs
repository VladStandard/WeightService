// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPrinterResources : RazorPageBase
{
	#region Public and private fields, properties, constructor

    private List<PrinterResourceModel> ItemsCast
    {
        get => Items is null ? new() : Items.Select(x => (PrinterResourceModel)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    public SectionPrinterResources()
    {
	    Table = new TableScaleModel(SqlTableScaleEnum.PrintersResources);
	    IsShowMarkedFilter = true;
	    ItemsCast = new();
    }

    #endregion

    #region Public and private methods

  //  protected override void OnInitialized()
  //  {
  //      base.OnInitialized();

		//RunActionsInitialized(new()
		//{
		//	() =>
		//	{
		//        //
		//	}
		//});
  //  }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
				ItemsCast = AppSettings.DataAccess.GetListPrinterResources(IsShowMarked, IsShowOnlyTop, Item);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
