// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPlusScales : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public List<PluScaleEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluScaleEntity)x).ToList();
		set => Items = value.Cast<BaseEntity>().ToList();
	}
	private ScaleEntity ItemFilterCast
	{
		get => ItemFilter == null ? new() : (ScaleEntity)ItemFilter;
		set => ItemFilter = value;
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.PlusScales);
		IsShowAdditionalFilter = true;
		Items = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				long scaleId = ItemFilterCast.IdentityId != 0 ? ItemFilterCast.IdentityId : 0;
				if (IsShowMarkedItems)
				{
					Items = AppSettings.DataAccess.Crud.GetEntities<PluScaleEntity>(
							scaleId == 0 ? null : new(new()
								{ new($"{nameof(PluScaleEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId) }),
							null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
							IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
						?.ToList<BaseEntity>();
				}
				else
				{
					Items = AppSettings.DataAccess.Crud.GetEntities<PluScaleEntity>(
							scaleId == 0 ? new(new() { new(DbField.IsMarked, DbComparer.Equal, false) })
								: new(new()
								{
									new(DbField.IsMarked, DbComparer.Equal, false),
									new($"{nameof(PluScaleEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId),
								}),
							null, //new($"{nameof(PluRefV2Entity.Plu)}.{DbField.IdentityId}"),
							IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
						?.ToList<BaseEntity>();
				}
				ButtonSettings = new(true, true, true, true, true, true, false);
			}
		});
	}

	#endregion
}
