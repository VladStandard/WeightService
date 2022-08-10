// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section.Measurements;

public partial class SectionWeithingFacts
{
    #region Public and private fields, properties, constructor

    private List<WeithingFactEntity> ItemsCast => Items == null ? new() : Items.Select(x => (WeithingFactEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionWeithingFacts()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.WeithingFacts);
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
                Items = AppSettings.DataAccess.Crud.GetEntities<WeithingFactEntity>(
                        (IsShowMarkedItems == true)
                            ? null
                            : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
                        new(DbField.WeithingDate, DbOrderDirection.Desc),
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.ToList<BaseEntity>();
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
