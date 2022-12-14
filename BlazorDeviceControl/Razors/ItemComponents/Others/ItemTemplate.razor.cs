// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Sql.TableScaleModels.Templates;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemTemplate : RazorComponentItemBase<TemplateModel>
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<string>>? TemplateCategories { get; }

	public ItemTemplate()
	{
		TemplateCategories = BlazorAppSettings.DataSourceDics.GetTemplateCategories();
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = DataContext.GetItemNotNullable<TemplateModel>(IdentityId);
                if (SqlItemCast.IdentityIsNew)
	                SqlItem = SqlItemNew<TemplateModel>();

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
