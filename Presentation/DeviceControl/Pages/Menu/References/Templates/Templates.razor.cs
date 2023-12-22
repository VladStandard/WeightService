namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class Templates : SectionBase<SqlTemplateEntity>
{
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlTemplateRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(SqlTemplateEntity templateEntity)
    {
        return templateEntity.Data.Length > 1024
            ? $"{templateEntity.Data.Length / 1024:### ##0} {Locale.DataSizeKBytes}"
            : $"{templateEntity.Data.Length:##0} {Locale.DataSizeBytes}";
    }
}
