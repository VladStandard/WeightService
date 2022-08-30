// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPlus : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public List<PluEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluEntity)x).ToList();
		set => Items = value.Cast<TableModel>().ToList();
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.Plus);
		IsShowMarkedFilter = true;
		Items = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.Crud.GetListPlus(IsShowMarked, IsShowOnlyTop);

				ButtonSettings = new(false, false, true, true, false, false, false);
			}
		});
	}

	#endregion
}
