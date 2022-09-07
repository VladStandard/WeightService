// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemHost : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private HostModel ItemCast { get => Item == null ? new() : (HostModel)Item; set => Item = value; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableScaleModel(ProjectsEnums.TableScale.Hosts);
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
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.SetDtNow();
						ItemCast.IsMarked = false;
						ItemCast.Name = "NEW HOST";
						ItemCast.Ip = "127.0.0.1";
						ItemCast.MacAddress.Default();
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<HostModel>(IdentityId);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
