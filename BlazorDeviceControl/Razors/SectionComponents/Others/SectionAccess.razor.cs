﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Access;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionAccess : RazorComponentSectionBase<AccessModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    public SectionAccess()
    {
		SqlCrudConfigSection.IsGuiShowItemsCount = true;
        SqlCrudConfigSection.IsGuiShowFilterMarked = true;
	    SqlCrudConfigSection.IsGuiShowFilterOnlyTop = true;
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlSectionCast = DataContext.GetListNotNullable<AccessModel>(SqlCrudConfigSection);

				ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
    }

    #endregion
}
