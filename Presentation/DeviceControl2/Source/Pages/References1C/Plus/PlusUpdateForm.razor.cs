using System.ComponentModel.DataAnnotations;
using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
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
    
    private TemplateEntity Template { get; set; } = new();
    private IEnumerable<TemplateEntity> AllTemplates { get; set; } = [];
    

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Template = PluService.GetPluTemplate(DialogItem);
        AllTemplates = TemplateService.GetAll();
    }

    protected override PluEntity UpdateItemAction() => DialogItem;

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];
}


public class PlusUpdateFormValidator : AbstractValidator<PluEntity>
{
    public PlusUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.FullName).NotEmpty();
        RuleFor(item => item.Description).NotEmpty();
    }
}