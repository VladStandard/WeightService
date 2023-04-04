// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Templates;

namespace BlazorDeviceControl.Pages.Menu.References.SectionTemplates;

public sealed partial class Templates : RazorComponentSectionBase<TemplateModel>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods
    
    private string ConvertBytes(TemplateModel templateModel)
    {
        return templateModel.Data.Length > 1024
            ? $"{templateModel.Data.Length / 1024:### ##0} {LocaleCore.Strings.DataSizeKBytes}"
            : $"{templateModel.Data.Length:##0} {LocaleCore.Strings.DataSizeBytes}";
    }
    
    #endregion
}
