using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.ZplResources;

namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;

public sealed partial class TemplateResourcesUpdateForm : SectionFormBase<ZplResource>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

    #endregion

    protected override ZplResource UpdateItemAction(ZplResource item) =>
        ZplResourceService.Update(item);

    protected override Task DeleteItemAction(ZplResource item)
    {
        ZplResourceService.Delete(DialogItemCopy.Uid, DialogItemCopy.Name);
        return Task.CompletedTask;
    }
}

public class TemplateResourcesUpdateFormValidator : AbstractValidator<ZplResource>
{
    public TemplateResourcesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}