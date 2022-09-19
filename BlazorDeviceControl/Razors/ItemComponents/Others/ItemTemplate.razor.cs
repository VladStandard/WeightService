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
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						SqlItemCast = new();
						SqlItemCast.SetDtNow();
						SqlItemCast.IsMarked = false;
						SqlItemCast.Title = "NEW TEMPLATE";
						SqlItemCast.IdRRef = Guid.Empty;
						SqlItemCast.CategoryId = "300 dpi";
						SqlItemCast.ImageData.SetTemplateValue();
						break;
					default:
						SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<TemplateModel>(IdentityId);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
