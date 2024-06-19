using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
using Ws.Desktop.Models.Features.PalletMen;

namespace ScalesDesktop.Source.Features;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    # region Injects

    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private PalletApi PalletApi { get; set; } = default!;
    [Inject] private ArmApi ArmApi { get; set; } = default!;

    # endregion

    [SupplyParameterFromForm] private PalletManFormModel FormModel { get; set; } = new();

    private void OnSubmit() => PalletContext.SetPalletMan(FormModel.User!);
}

public class PalletManFormModel {
    public PalletMan? User { get; set; }
    public string Password { get; set; } = string.Empty;
}

public class PalletManFormValidator : AbstractValidator<PalletManFormModel>
{
    public PalletManFormValidator(IStringLocalizer<ApplicationResources> localizer)
    {
        RuleFor(item => item.User).NotNull();
        RuleFor(item => item.Password).NotNull().Length(4);
        RuleFor(item => item.Password)
            .Must((item, password) => item.User != null && password == item.User.Password)
            .WithMessage(localizer["PalletManFormInvalidPassword"]);
    }
}