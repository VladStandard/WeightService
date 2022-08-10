// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemAccess
{
	#region Public and private fields, properties, constructor

	private AccessEntity ItemCast { get => Item == null ? new() : (AccessEntity)Item; set => Item = value; }
	private List<TypeEntity<AccessRights>>? TemplateAccessRights { get; set; }

	private AccessRights Rights
	{
		get => ItemCast == null ? AccessRights.None : (AccessRights)ItemCast.Rights;
		set { if (ItemCast != null) ItemCast.Rights = (byte)value; }
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
		ItemCast = new();
		TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
						ItemCast.IsMarked = false;
						ItemCast.User = "NEW USER";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
							new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
						break;
				}

				TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights(ItemCast.Rights);
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
