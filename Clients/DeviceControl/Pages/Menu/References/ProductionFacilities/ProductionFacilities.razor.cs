// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.ProductionFacilities;

public sealed partial class ProductionFacilities : SectionBase<WsSqlProductionFacilityModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlAreaRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
