// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemProductionFacility
{
    #region Public and private fields, properties, constructor

    private ProductionFacilityEntity ItemCast { get => Item == null ? new() : (ProductionFacilityEntity)Item; set => Item = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemProductionFacility()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.ProductionFacilities);
        ItemCast = new();
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
					new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
				if (IdentityId != null && TableAction == DbTableAction.New)
					ItemCast.IdentityId = (long)IdentityId;
				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
