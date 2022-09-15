// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents;

public partial class ItemAccess : RazorPageItemBase<AccessModel>
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<AccessRightsEnum>> TemplateAccessRights { get; set; }

	private AccessRightsEnum Rights
	{
		get => (AccessRightsEnum)ItemCast.Rights;
		set => ItemCast.Rights = (byte)value;
	}

	public ItemAccess()
	{
		TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
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
