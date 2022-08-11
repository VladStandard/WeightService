// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsFilterScale
{
	#region Public and private fields, properties, constructor

	private List<ScaleEntity> ItemsCast => ItemsFilter == null ? new() : ItemsFilter.Select(x => (ScaleEntity)x).ToList();

	private ScaleEntity ItemFilterCast
	{
		get => ItemFilter == null ? new() : (ScaleEntity)ItemFilter;
		set
		{
			ItemFilter = value;
			if (ParentRazor != null)
			{
				ParentRazor.ItemFilter = ItemFilter;
			}
		}
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		RunActions(new()
		{
			() =>
			{
				ScaleEntity[]? itemsFilter = AppSettings.DataAccess.Crud.GetEntities<ScaleEntity>(
					new(new() { new(ShareEnums.DbField.IsMarked, ShareEnums.DbComparer.Equal, false) }),
					new(ShareEnums.DbField.Description));
				if (itemsFilter is { })
				{
					ItemsFilter = new();
					ItemsFilter.Add(new ScaleEntity(0) { Description = LocaleCore.Table.FieldNull });
					ItemsFilter.AddRange(itemsFilter.ToList<BaseEntity>());
					if (ItemFilter == null)
						ItemFilterCast = ItemsCast.First();
				}
			}
		});
	}

	#endregion
}
