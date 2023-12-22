namespace DeviceControl.Pages.Menu.References.TemplateResources;

public sealed partial class TemplateResources : SectionBase<SqlTemplateResourceEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlTemplateResourceRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(SqlTemplateResourceEntity templateResource)
    {
        return templateResource.DataValue.Length > 1024
            ? $"{templateResource.DataValue.Length / 1024:### ##0} {Locale.DataSizeKBytes}"
            : $"{templateResource.DataValue.Length:##0} {Locale.DataSizeBytes}";
    }
}
