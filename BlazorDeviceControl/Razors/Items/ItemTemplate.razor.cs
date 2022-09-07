// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemTemplate : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private TemplateModel ItemCast { get => Item is null ? new() : (TemplateModel)Item; set => Item = value; }
	private List<TypeModel<string>>? TemplateCategories { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableScaleModel(SqlTableScaleEnum.Templates);
				TemplateCategories = AppSettings.DataSourceDics.GetTemplateCategories();
				ItemCast = new();
			}
		});
	}

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
