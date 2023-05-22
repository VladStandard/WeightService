// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.ProductionFacilities;

namespace BlazorDeviceControl.Pages.Menu.References.SectionProductionFacilities;

public sealed partial class ItemProductionFacility : RazorComponentItemBase<WsSqlProductionFacilityModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<WsSqlProductionFacilityModel>(IdentityId);
        if (SqlItemCast.IsNew)
            SqlItemCast = SqlItemNew<WsSqlProductionFacilityModel>();
    }
    #endregion
}
