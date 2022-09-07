// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections;

/// <summary>
/// Section ProductionFacilities.
/// </summary>
public partial class SectionProductionFacilities : RazorPageBase
{
    #region Public and private fields, properties, constructor

    private List<ProductionFacilityModel> ItemsCast
    {
        get => Items == null ? new() : Items.Select(x => (ProductionFacilityModel)x).ToList();
        set => Items = !value.Any() ? null : new(value);
    }

    #endregion

    #region Public and private methods

    protected override void OnInitialized()
    {
        base.OnInitialized();

        RunActionsInitialized(new()
        {
	        () =>
	        {
		        Table = new TableScaleModel(ProjectsEnums.TableScale.ProductionFacilities);
		        IsShowMarkedFilter = true;
				ItemsCast = new();
	        }
        });
    }

    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        RunActionsParametersSet(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.GetListProductionFacilities(IsShowMarked, IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
