// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Access;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemAccess : RazorComponentItemBase<AccessModel>
{
	#region Public and private fields, properties, constructor

	private List<TypeModel<AccessRightsEnum>> TemplateAccessRights { get; set; }

	private AccessRightsEnum Rights
	{
		get => (AccessRightsEnum)SqlItemCast.Rights;
		set => SqlItemCast.Rights = (byte)value;
	}

	public ItemAccess()
	{
		TemplateAccessRights = BlazorAppSettings.DataSourceDics.GetTemplateAccessRights();
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				SqlItemCast = DataContext.GetItemNotNullable<AccessModel>(IdentityUid);
                if (SqlItemCast.IsNew)
				{
					SqlItemCast = SqlItemNew<AccessModel>();
				}
				TemplateAccessRights = BlazorAppSettings.DataSourceDics.GetTemplateAccessRights(SqlItemCast.Rights);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
