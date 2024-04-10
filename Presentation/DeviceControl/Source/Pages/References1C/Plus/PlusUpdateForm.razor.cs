using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.References1C.Plus;

public sealed partial class PlusUpdateForm : SectionFormBase<PluEntity>
{
    # region Injects

    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    private TemplateEntity Template { get; set; } = new();
    private IEnumerable<TemplateEntity> AllTemplates { get; set; } = [];


    protected override void OnInitialized()
    {
        base.OnInitialized();
        // Template = PluService.GetPluTemplate(DialogItem);
        // AllTemplates = TemplateService.GetAll();
    }

    protected override PluEntity UpdateItemAction(PluEntity item) => item;

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColPluWeight"] : WsDataLocalizer["ColPluPiece"];
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