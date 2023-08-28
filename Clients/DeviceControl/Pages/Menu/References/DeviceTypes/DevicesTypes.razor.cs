namespace DeviceControl.Pages.Menu.References.DeviceTypes;

public sealed partial class DevicesTypes : SectionBase<WsSqlDeviceTypeModel>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlDeviceTypeRepository().GetList(SqlCrudConfigSection);
    }
}
