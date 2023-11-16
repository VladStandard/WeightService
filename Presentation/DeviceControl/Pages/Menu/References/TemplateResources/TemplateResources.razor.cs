namespace DeviceControl.Pages.Menu.References.TemplateResources;

public sealed partial class TemplateResources : SectionBase<SqlTemplateResourceEntity>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlTemplateResourceRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(SqlTemplateResourceEntity templateResource)
    {
        return templateResource.DataValue.Length > 1024
            ? $"{templateResource.DataValue.Length / 1024:### ##0} {LocaleCore.Strings.DataSizeKBytes}"
            : $"{templateResource.DataValue.Length:##0} {LocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}
