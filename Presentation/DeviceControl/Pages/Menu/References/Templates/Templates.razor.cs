namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class Templates : SectionBase<SqlTemplateEntity>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlTemplateRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(SqlTemplateEntity templateEntity)
    {
        return templateEntity.Data.Length > 1024
            ? $"{templateEntity.Data.Length / 1024:### ##0} {LocaleCore.Strings.DataSizeKBytes}"
            : $"{templateEntity.Data.Length:##0} {LocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}
