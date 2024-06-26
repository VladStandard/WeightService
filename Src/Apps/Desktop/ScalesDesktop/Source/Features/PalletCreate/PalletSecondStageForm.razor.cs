using FluentValidation;
using Microsoft.AspNetCore.Components;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletSecondStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }
}

public class PalletSecondStageFormValidator : AbstractValidator<PalletCreateModel>
{
    public PalletSecondStageFormValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.PalletWeight).GreaterThan(0).LessThanOrEqualTo(999).WithName(wsDataLocalizer["ColPalletWeight"]);
        RuleFor(item => item.Count).GreaterThanOrEqualTo(1).LessThanOrEqualTo(240).WithName(wsDataLocalizer["ColCount"]);
        RuleFor(item => item.Kneading).GreaterThanOrEqualTo((short)1).LessThanOrEqualTo((short)999).WithName(wsDataLocalizer["ColKneading"]);
    }
}