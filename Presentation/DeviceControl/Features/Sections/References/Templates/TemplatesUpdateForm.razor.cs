using DeviceControl.Features.Sections.Shared;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaScale.Templates;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesUpdateForm: SectionFormBase<SqlTemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private IEnumerable<TemplateCategoriesEnum> PrinterTypesEntities { get; set; } = new List<TemplateCategoriesEnum>();
    private TemplateCategoriesEnum SelectedCategory { get; set; } = TemplateCategoriesEnum.Weight;

    protected override void OnInitialized()
    {
        SelectedCategory = EnumHelper.GetValueFromDescription<TemplateCategoriesEnum>(SectionEntity.CategoryId);
        PrinterTypesEntities = Enum.GetValues(typeof(TemplateCategoriesEnum)).Cast<TemplateCategoriesEnum>().ToList();
    }

    private TemplateCategoriesEnum GetTemplateCategoryByValue(string categoryValue)
    {
        
        TemplateCategoriesEnum category = categoryValue.ToEnum<TemplateCategoriesEnum>();
        SectionEntity.CategoryId = EnumHelper.GetEnumDescription(category);
        return category;
    }
}
