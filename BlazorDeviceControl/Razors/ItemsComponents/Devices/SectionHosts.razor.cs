﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemsComponents.Devices;

public partial class SectionHosts : RazorPageSectionBase<HostModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionHosts()
    {
	    RazorPageConfig.IsShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
		RunActionsParametersSet(new()
        {
            () =>
            {
	            ItemsCast = AppSettings.DataAccess.GetListHosts(RazorPageConfig.IsShowMarked, RazorPageConfig.IsShowOnlyTop, false);
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}
