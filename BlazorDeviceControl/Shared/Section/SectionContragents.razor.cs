// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionContragents
{
    #region Public and private fields, properties, constructor

    private List<ContragentV2Entity> ItemsCast => Items == null ? new() : Items.Select(x => (ContragentV2Entity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionContragents()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);
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
                Items = AppSettings.DataAccess.Crud.GetEntities<ContragentV2Entity>(
                    (IsShowMarkedItems == true) ? null
                        : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                    new(DbField.Name, DbOrderDirection.Asc),
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
