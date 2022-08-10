// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemWorkshop
{
    #region Public and private fields, properties, constructor

    private WorkShopEntity ItemCast { get => Item == null ? new() : (WorkShopEntity)Item; set => Item = value; }
    private List<ProductionFacilityEntity> ProductionFacilities { get; set; }

    public ItemWorkshop()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Workshops);
        ItemCast = new();
        ProductionFacilities = new();
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
				ItemCast = AppSettings.DataAccess.Crud.GetEntity<WorkShopEntity>(
					new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
				if (IdentityId != null && TableAction == DbTableAction.New)
					ItemCast.IdentityId = (long)IdentityId;
				// ProductionFacilities.
				List<ProductionFacilityEntity>? productionFacilities = AppSettings.DataAccess.Crud
					.GetEntities<ProductionFacilityEntity>(
						new(new() { new(DbField.IsMarked, DbComparer.Equal, false) }),
						new(DbField.Name, DbOrderDirection.Asc))
					?.ToList();
				if (productionFacilities is not null)
				{
					ProductionFacilities.AddRange(productionFacilities.Where(x => x.IdentityId > 0).ToList());
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
