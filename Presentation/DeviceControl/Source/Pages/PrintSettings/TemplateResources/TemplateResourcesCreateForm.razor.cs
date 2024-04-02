using DeviceControl.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;

public sealed partial class TemplateResourcesCreateForm : SectionFormBase<ZplResourceEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

    #endregion

    protected override ZplResourceEntity CreateItemAction(ZplResourceEntity item) =>
        ZplResourceService.Create(item);
}

public class TemplateResourcesCreateFormValidator : AbstractValidator<ZplResourceEntity>
{
    public TemplateResourcesCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}