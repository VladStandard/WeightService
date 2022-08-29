// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Fields;
using DataCore.Sql.Tables;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionPlusScales : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	[Parameter] public List<PluScaleEntity> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (PluScaleEntity)x).ToList();
		set => Items = value.Cast<TableModel>().ToList();
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
				long? scaleId = null;
				if (ItemFilter is ScaleEntity scale)
					scaleId = scale.IdentityId;
				List<FieldFilterModel> filters = IsShowMarkedFilter ? new() : new List<FieldFilterModel> { new(DbField.IsMarked, DbComparer.Equal, false) };
				if (scaleId is not null)
					filters.Add(new($"{nameof(PluScaleEntity.Scale)}.{DbField.IdentityId}", DbComparer.Equal, scaleId));
				ItemsCast = AppSettings.DataAccess.Crud.GetItemsListNotNull<PluScaleEntity>(IsShowOnlyTop, filters, new(DbField.GoodsName));

				ButtonSettings = new(true, true, true, true, true, true, false);
			}
		});
	}

	#endregion
}
