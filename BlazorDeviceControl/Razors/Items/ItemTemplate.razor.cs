// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemTemplate : BlazorCore.Models.RazorBase
{
	#region Public and private fields, properties, constructor

	private TemplateEntity ItemCast { get => Item == null ? new() : (TemplateEntity)Item; set => Item = value; }
	private List<TypeEntity<string>>? TemplateCategories { get; set; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.Templates);
		TemplateCategories = BlazorCore.Models.DataSourceDicsEntity.GetTemplateCategories();
		ItemCast = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActions(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
						ItemCast.IsMarked = false;
						ItemCast.Title = "NEW TEMPLATE";
						ItemCast.IdRRef = Guid.Empty;
						ItemCast.CategoryId = "300 dpi";
						ItemCast.ImageData.SetTemplateValue();
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<TemplateEntity>(
							new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
