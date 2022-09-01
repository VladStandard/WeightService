﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

/// <summary>
/// Section ProductionFacilities.
/// </summary>
public partial class SectionProductionFacilities : RazorPageModel
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

        Table = new TableScaleModel(ProjectsEnums.TableScale.ProductionFacilities);
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
	            ItemsCast = AppSettings.DataAccess.Crud.GetListProductionFacilities(IsShowMarked, IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
