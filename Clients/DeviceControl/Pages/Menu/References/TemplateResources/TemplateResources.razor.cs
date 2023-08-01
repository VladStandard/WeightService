// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.TemplateResources;

public sealed partial class TemplateResources : SectionBase<WsSqlTemplateResourceModel>
{
    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlTemplateResourceRepository().GetList(SqlCrudConfigSection);
    }
    
    private static string ConvertBytes(WsSqlTemplateResourceModel templateResource)
    {
        return templateResource.DataValue.Length > 1024
            ? $"{templateResource.DataValue.Length / 1024:### ##0} {WsLocaleCore.Strings.DataSizeKBytes}"
            : $"{templateResource.DataValue.Length:##0} {WsLocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}