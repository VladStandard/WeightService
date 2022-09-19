// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemNomenclature : RazorComponentItemBase<NomenclatureModel>
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
		RunActionsParametersSet(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						SqlItemCast = new();
						SqlItemCast.SetDtNow();
						SqlItemCast.Name = "NEW NOMENCLATURE";
						break;
					default:
						SqlItemCast = AppSettings.DataAccess.GetItemByIdNotNull<NomenclatureModel>(IdentityId);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
