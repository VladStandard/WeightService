using FluentValidation;
using Microsoft.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletFirstStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private ArmApi ArmApi { get; set; } = default!;
    [Inject] private PluApi PluApi { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnValidSubmit { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }

    private void OnPluSelected() => FormModel.Nesting =
        FormModel.Plu!.Nestings.Find(x => x.Id == Guid.Empty);
}

public class PalletPluStageFormValidator : AbstractValidator<PalletCreateModel>
{
    public PalletPluStageFormValidator()
    {
        RuleFor(item => item.Plu).NotNull();
        RuleFor(item => item.Nesting).NotNull();
    }
}