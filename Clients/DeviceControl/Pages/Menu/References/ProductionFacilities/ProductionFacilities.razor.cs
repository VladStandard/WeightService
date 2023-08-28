namespace DeviceControl.Pages.Menu.References.ProductionFacilities;

public sealed partial class ProductionFacilities : SectionBase<WsSqlProductionFacilityModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlAreaRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }
}
