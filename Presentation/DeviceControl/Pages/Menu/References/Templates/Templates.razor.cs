namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class Templates : SectionBase<WsSqlTemplateEntity>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlTemplateRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(WsSqlTemplateEntity templateEntity)
    {
        return templateEntity.Data.Length > 1024
            ? $"{templateEntity.Data.Length / 1024:### ##0} {WsLocaleCore.Strings.DataSizeKBytes}"
            : $"{templateEntity.Data.Length:##0} {WsLocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}
