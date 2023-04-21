// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.TemplatesResources;

namespace BlazorDeviceControl.Pages.Menu.References.SectionTemplateResources;

public sealed partial class TemplateResources : RazorComponentSectionBase<TemplateResourceModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    private string ConvertBytes(TemplateResourceModel templateResource)
    {
        return templateResource.DataValue.Length > 1024
            ? $"{templateResource.DataValue.Length / 1024:### ##0} {LocaleCore.Strings.DataSizeKBytes}"
            : $"{templateResource.DataValue.Length:##0} {LocaleCore.Strings.DataSizeBytes}";
    }

    #endregion
}