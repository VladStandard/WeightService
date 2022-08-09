// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsFilterScale : BlazorCore.Models.RazorBase
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

	public override async Task SetParametersAsync(ParameterView parameters)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
		SetParametersAsyncWithAction(parameters, () => base.SetParametersAsync(parameters).ConfigureAwait(true),
			null, () =>
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
			});
	}

	#endregion
}
