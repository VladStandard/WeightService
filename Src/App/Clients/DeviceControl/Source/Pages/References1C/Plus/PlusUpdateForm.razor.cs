using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Features.Plu;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl.Source.Pages.References1C.Plus;

public sealed partial class PlusUpdateForm : SectionFormBase<PluEntity>
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

    protected override PluEntity UpdateItemAction(PluEntity item) => PluService.Update(item);

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