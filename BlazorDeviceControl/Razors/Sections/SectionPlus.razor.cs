// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPlus : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public List<PluEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluEntity)x).ToList();
		set => Items = value.Cast<BaseEntity>().ToList();
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
				Items = AppSettings.DataAccess.Crud.GetEntities<PluEntity>(
						IsShowMarkedItems ? null : new FilterListEntity(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
						new(DbField.Name),
						IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
					?.ToList<BaseEntity>();
				ButtonSettings = new(false, false, true, true, false, false, false);
			}
		});
	}

	#endregion
}
