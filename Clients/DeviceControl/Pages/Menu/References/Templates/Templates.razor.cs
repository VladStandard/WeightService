// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.Templates;

namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class Templates : SectionBase<WsSqlTemplateModel>
{
    #region Public and private methods

    private static string ConvertBytes(WsSqlTemplateModel templateModel)
    {
        return templateModel.Data.Length > 1024
            ? $"{templateModel.Data.Length / 1024:### ##0} {WsLocaleCore.Strings.DataSizeKBytes}"
            : $"{templateModel.Data.Length:##0} {WsLocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}