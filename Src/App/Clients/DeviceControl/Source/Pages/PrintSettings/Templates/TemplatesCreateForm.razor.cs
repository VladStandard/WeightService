using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl.Source.Pages.PrintSettings.Templates;

public sealed partial class TemplatesCreateForm : SectionFormBase<Template>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;

    #endregion

    protected override Template CreateItemAction(Template item) =>
        TemplateService.Create(item);

    private string GetTemplateTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColTemplateWeight"] : WsDataLocalizer["ColTemplatePiece"];
}

public class TemplatesCreateFormValidator : AbstractValidator<Template>
{
    public TemplatesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Body).NotEmpty();
    }
}