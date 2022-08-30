// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemContragent : RazorBase
{
	#region Public and private fields, properties, constructor

	private ContragentEntity ItemCast { get => Item == null ? new() : (ContragentEntity)Item; set => Item = value; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);
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
						ItemCast.Name = "NEW CONTRAGENT";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetItemByUidNotNull<ContragentEntity>(IdentityUid);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
