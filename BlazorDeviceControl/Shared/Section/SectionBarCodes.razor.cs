// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionBarCodes
{
    #region Public and private fields, properties, constructor

    private List<BarCodeV2Entity> ItemsCast => Items == null ? new() : Items.Select(x => (BarCodeV2Entity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionBarCodes()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.BarCodes);
        Items = new();
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
	    base.OnParametersSet();
	    SetParametersWithAction(new()
	    {
		    () =>
		    {
			    Items = AppSettings.DataAccess.Crud.GetEntities<BarCodeV2Entity>(
					    (IsShowMarkedItems == true)
						    ? null
						    : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
					    new(DbField.Value, DbOrderDirection.Asc),
					    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
				    ?.ToList<BaseEntity>();
			    ButtonSettings = new(true, true, true, true, true, false, false);
		    }
	    });
	}

    #endregion
}
