// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemLog : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private LogModel ItemCast { get => Item == null ? new() : (LogModel)Item; set => Item = value; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableSystemModel(SqlTableSystemEnum.Logs);
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
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByUidNotNull<LogModel>(IdentityUid);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, false, true);
			}
		});
	}

	#endregion
}
