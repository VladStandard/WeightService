// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class ActionsFilterScale : RazorBase
{
	#region Public and private fields, properties, constructor

	private List<ScaleEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (ScaleEntity)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

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
				ItemsFilter = new() { new ScaleEntity(0, false) { Description = LocaleCore.Table.FieldNull } };
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
				ScaleEntity[]? itemsFilter = AppSettings.DataAccess.Crud.GetItems<ScaleEntity>(sqlCrudConfig);
				if (itemsFilter is not null)
				{
					ItemsFilter.AddRange(itemsFilter.ToList<TableModel>());
					if (ParentRazor?.ItemFilter == null)
						ItemFilterCast = ItemsCast.First();
				}
			}
		});
	}

	#endregion
}
