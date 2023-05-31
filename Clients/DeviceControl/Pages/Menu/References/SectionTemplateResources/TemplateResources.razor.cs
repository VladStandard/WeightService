// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TemplatesResources;

namespace DeviceControl.Pages.Menu.References.SectionTemplateResources;

public sealed partial class TemplateResources : RazorComponentSectionBase<WsSqlTemplateResourceModel>
{
    #region Public and private methods

    private string ConvertBytes(WsSqlTemplateResourceModel templateResource)
    {
        return templateResource.DataValue.Length > 1024
            ? $"{templateResource.DataValue.Length / 1024:### ##0} {LocaleCore.Strings.DataSizeKBytes}"
            : $"{templateResource.DataValue.Length:##0} {LocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}
