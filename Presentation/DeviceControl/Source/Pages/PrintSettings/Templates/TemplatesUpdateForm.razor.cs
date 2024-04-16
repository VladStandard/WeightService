using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.PrintSettings.Templates;

public sealed partial class TemplatesUpdateForm : SectionFormBase<TemplateEntity>
{
    #region Inject

    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;

    #endregion

    protected override TemplateEntity UpdateItemAction(TemplateEntity item) =>
        TemplateService.Update(item);

    protected override Task DeleteItemAction(TemplateEntity item)
    {
        TemplateService.Delete(item);
        return Task.CompletedTask;
    }

    private string GetTemplateTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColTemplateWeight"] : WsDataLocalizer["ColTemplatePiece"];
}

public class TemplatesUpdateFormValidator : AbstractValidator<TemplateEntity>
{
    public TemplatesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Body).NotEmpty();
    }
}