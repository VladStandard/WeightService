// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemNomenclature : RazorPageItemBase<NomenclatureModel>
{
	#region Public and private fields, properties, constructor

	public ItemNomenclature()
	{
		//
	}

	#endregion

	#region Public and private methods

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
						ItemCast.Name = "NEW NOMENCLATURE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<NomenclatureModel>(IdentityId);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
