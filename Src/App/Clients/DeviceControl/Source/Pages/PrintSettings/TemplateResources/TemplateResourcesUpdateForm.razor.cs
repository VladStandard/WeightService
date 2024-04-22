using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;

public sealed partial class TemplateResourcesUpdateForm : SectionFormBase<ZplResourceEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

    #endregion

    protected override ZplResourceEntity UpdateItemAction(ZplResourceEntity item) =>
        ZplResourceService.Update(item);

    protected override Task DeleteItemAction(ZplResourceEntity item)
    {
        ZplResourceService.Delete(item);
        return Task.CompletedTask;
    }
}

public class TemplateResourcesUpdateFormValidator : AbstractValidator<ZplResourceEntity>
{
    public TemplateResourcesUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}