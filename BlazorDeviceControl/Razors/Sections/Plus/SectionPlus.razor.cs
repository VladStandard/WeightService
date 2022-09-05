// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections.Plus;

public partial class SectionPlus : RazorPageModel
{
	#region Public and private fields, properties, constructor

	[Parameter] public List<PluModel> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluModel)x).ToList();
		set => Items = value.Cast<TableModel>().ToList();
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleModel(ProjectsEnums.TableScale.Plus);
		IsShowMarkedFilter = true;
		Items = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsSilent(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.GetListPlus(IsShowMarked, IsShowOnlyTop, false);

				ButtonSettings = new(false, false, true, true, false, false, false);
			}
		});
	}

	#endregion
}
