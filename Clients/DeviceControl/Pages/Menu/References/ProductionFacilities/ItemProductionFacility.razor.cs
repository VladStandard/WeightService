// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsStorageCore.TableScaleModels.ProductionFacilities;

namespace DeviceControl.Pages.Menu.References.ProductionFacilities;

public sealed partial class ItemProductionFacility : ItemBase<WsSqlProductionFacilityModel>
{
    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlProductionFacilityModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlProductionFacilityModel>();
    }
    #endregion
}