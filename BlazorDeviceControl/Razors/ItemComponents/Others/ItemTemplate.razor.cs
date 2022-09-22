// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemTemplate : RazorComponentItemBase<TemplateModel>
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<string>>? TemplateCategories { get; set; }

	public ItemTemplate()
	{
		TemplateCategories = AppSettings.DataSourceDics.GetTemplateCategories();
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
                SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<TemplateModel>(IdentityId);
                if (SqlItemCast.Identity.IsNew())
	                SqlItem = SqlItemNew<TemplateModel>();

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
