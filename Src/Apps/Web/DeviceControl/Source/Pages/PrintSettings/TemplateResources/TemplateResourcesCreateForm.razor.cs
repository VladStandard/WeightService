using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.ZplResources;

namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;

public sealed partial class TemplateResourcesCreateForm : SectionFormBase<ZplResource>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

    #endregion

    protected override ZplResource CreateItemAction(ZplResource item) =>
        ZplResourceService.Create(item);
}

public class TemplateResourcesCreateFormValidator : AbstractValidator<ZplResource>
{
    public TemplateResourcesCreateFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Zpl).NotEmpty().WithName(wsDataLocalizer["ColData"]);
    }
}