using System.ComponentModel.DataAnnotations;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl2.Source.Pages.References1C.Plus;

public sealed partial class PlusUpdateForm: SectionFormBase<PluEntity>
{
    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    
    [SupplyParameterFromForm]
    private PlusUpdateFormModel FormModel { get; set; } = new();
    private PlusUpdateFormModel FormModelCopy { get; set; } = new();
    private TemplateEntity Template { get; set; } = new();
    private IEnumerable<TemplateEntity> AllTemplates { get; set; } = [];
    

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Template = PluService.GetPluTemplate(SectionEntity);
        AllTemplates = TemplateService.GetAll();
        
        FormModel.Description = SectionEntity.Description;
        FormModel.Name = SectionEntity.Name;
        FormModel.FullName = SectionEntity.FullName;
        FormModel.Template = Template;
        
        FormModelCopy = FormModel.DeepClone();
    }

    private async Task UpdatePluEntity()
    {
        SectionEntity.Description = FormModel.Description;
        SectionEntity.Name = FormModel.Name;
        SectionEntity.FullName = FormModel.FullName;
        if (FormModel.Equals(FormModelCopy))
        {
            await OnCancelAction.InvokeAsync();
            return;
        }
        await OnSubmitAction.InvokeAsync();
    }

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];
}

public record PlusUpdateFormModel
{
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string FullName { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public TemplateEntity? Template { get; set; }
}