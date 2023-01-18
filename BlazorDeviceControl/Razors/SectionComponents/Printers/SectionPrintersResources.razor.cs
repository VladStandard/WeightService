// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.PrintersResources;

namespace BlazorDeviceControl.Razors.SectionComponents.Printers;

public partial class SectionPrintersResources : RazorComponentSectionBase<PrinterResourceModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionPrintersResources() : base()
    {
        SqlCrudConfigSection.IsGuiShowFilterOnlyTop = false;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlCrudConfigSection.AddFilters(nameof(PrinterResourceModel.Printer), ParentRazor ?.SqlItem);
				SqlSectionCast = DataContext.GetListNotNullable<PrinterResourceModel>(SqlCrudConfigSection);
            }
        });
    }

    #endregion
}