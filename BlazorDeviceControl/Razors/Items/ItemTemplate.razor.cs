// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemTemplate : RazorPageItemBase<TemplateModel>
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
		base.OnParametersSet();

		RunActionsParametersSet(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Title = "NEW TEMPLATE";
						ItemCast.IdRRef = Guid.Empty;
						ItemCast.CategoryId = "300 dpi";
						ItemCast.ImageData.SetTemplateValue();
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<TemplateModel>(IdentityId);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
