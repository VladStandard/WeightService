// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsFilterScale : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private List<ScaleEntity> ItemsCast => ItemsFilter == null ? new() : ItemsFilter.Select(x => (ScaleEntity)x).ToList();

	private ScaleEntity ItemFilterCast
	{
		get
		{
			if (ParentRazor?.ParentRazor != null)
				return ParentRazor.ParentRazor.ItemFilter == null ? new() : (ScaleEntity)ParentRazor.ParentRazor.ItemFilter;
			return new();
		}
		set
		{
			if (ParentRazor?.ParentRazor != null)
			{
				ParentRazor.ParentRazor.ItemFilter = value;
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
				if (itemsFilter is not null)
				{
					ItemsFilter = new() { new ScaleEntity(0, false) { Description = LocaleCore.Table.FieldNull } };
					ItemsFilter.AddRange(itemsFilter.ToList<BaseEntity>());
					if (ParentRazor?.ItemFilter == null)
						ItemFilterCast = ItemsCast.First();
				}
			}
		});
	}

	#endregion
}
