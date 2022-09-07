// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemAccess : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private AccessModel ItemCast { get => Item is null ? new() : (AccessModel)Item; set => Item = value; }
	private List<TypeModel<AccessRightsEnum>>? TemplateAccessRights { get; set; }

	private AccessRightsEnum Rights
	{
		get => (AccessRightsEnum)ItemCast.Rights;
		set => ItemCast.Rights = (byte)value;
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableSystemModel(SqlTableSystemEnum.Accesses);
				ItemCast = new();
				TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
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
