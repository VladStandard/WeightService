// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemAccess : RazorPageModel
{
	#region Public and private fields, properties, constructor

	private AccessModel ItemCast { get => Item == null ? new() : (AccessModel)Item; set => Item = value; }
	private List<TypeModel<AccessRights>>? TemplateAccessRights { get; set; }

	private AccessRights Rights
	{
		get => (AccessRights)ItemCast.Rights;
		set => ItemCast.Rights = (byte)value;
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableSystemModel(ProjectsEnums.TableSystem.Accesses);
		ItemCast = new();
		TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsSilent(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.User = "NEW USER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByUidNotNull<AccessModel>(IdentityUid);
						break;
				}
				TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights(ItemCast.Rights);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
