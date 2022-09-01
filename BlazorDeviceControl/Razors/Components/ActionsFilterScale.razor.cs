// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Components;

public partial class ActionsFilterScale : RazorPageModel
{
	#region Public and private fields, properties, constructor

	private List<ScaleModel> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (ScaleModel)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	private ScaleModel ItemFilterCast
	{
		get
		{
			if (ParentRazor?.ParentRazor != null)
				return ParentRazor.ParentRazor.ItemFilter == null ? new() : (ScaleModel)ParentRazor.ParentRazor.ItemFilter;
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
				ItemsFilter = new() { new ScaleModel() { Description = LocaleCore.Table.FieldNull } };
				SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(null, new(DbField.Description), 0, false, false);
				ScaleModel[]? itemsFilter = AppSettings.DataAccess.Crud.GetItems<ScaleModel>(sqlCrudConfig);
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
