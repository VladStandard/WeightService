// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Components;

public partial class ActionsFilterScale : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private List<ScaleModel> ItemsCast
	{
		get => Items is null ? new() : Items.Select(x => (ScaleModel)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	private ScaleModel ItemFilterCast { get => Item is null ? new() : (ScaleModel)Item; set => Item = value; }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemsFilter = new() { new ScaleModel() { Description = LocaleCore.Table.FieldNull } };
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(SqlFieldEnum.Description), 0, false, false);
				ScaleModel[]? itemsFilter = AppSettings.DataAccess.GetItems<ScaleModel>(sqlCrudConfig);
				if (itemsFilter is not null)
				{
					ItemsFilter.AddRange(itemsFilter.ToList<TableBaseModel>());
					if (ItemFilterCast.EqualsDefault())
						ItemFilterCast = ItemsCast.First();
				}
			}
		});
	}

	#endregion
}
