// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Items;

public partial class ItemProductionFacility : RazorPageBase
{
	#region Public and private fields, properties, constructor

	private ProductionFacilityModel ItemCast { get => Item == null ? new() : (ProductionFacilityModel)Item; set => Item = value; }

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		RunActionsInitialized(new()
		{
			() =>
			{
				Table = new TableScaleModel(ProjectsEnums.TableScale.ProductionFacilities);
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
				ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<ProductionFacilityModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	ItemCast.Identity.Id = (long)IdentityId;
				
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
