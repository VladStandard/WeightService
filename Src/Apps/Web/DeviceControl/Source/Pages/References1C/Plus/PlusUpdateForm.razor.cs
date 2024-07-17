using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref1c.Plus;
using Ws.Domain.Services.Features.Plus;
using Ws.Domain.Services.Features.Templates;

namespace DeviceControl.Source.Pages.References1C.Plus;

public sealed partial class PlusUpdateForm : SectionFormBase<Plu>
{
    # region Injects

    [Inject] private Redirector Redirector { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

    private IEnumerable<Template> AllTemplates { get; set; } = [];


    protected override void OnInitialized()
    {
        base.OnInitialized();
        AllTemplates = TemplateService.GetTemplatesByIsWeight(DialogItem.IsCheckWeight);
    }

    protected override Plu UpdateItemAction(Plu item) => PluService.Update(item);

    private string GetPluTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColPluWeight"] : WsDataLocalizer["ColPluPiece"];
}


public class PlusUpdateFormValidator : AbstractValidator<Plu>
{
    public PlusUpdateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.FullName).NotEmpty().WithName(wsDataLocalizer["ColFullName"]);
        RuleFor(item => item.Description).NotEmpty().WithName(wsDataLocalizer["ColDescription"]);
    }
}