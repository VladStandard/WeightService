using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Api;
using ScalesDesktop.Source.Shared.Services;
using Ws.Desktop.Models.Features.PalletMen;

namespace ScalesDesktop.Source.Features;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    # region Injects

    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;

    # endregion

    [SupplyParameterFromForm] private PalletManFormModel FormModel { get; set; } = new();
    private IEnumerable<PalletMan> PalletMen { get; set; } = [];

    protected override async Task OnInitializedAsync()
    {
        try
        {
            PalletMen = await DesktopApi.GetPalletMen();
        }
        catch
        {
            // pass
        }
    }

    private void HandleInvalidForm(EditContext context)
    {
        foreach (string msg in context.GetValidationMessages())
            ToastService.ShowError(msg);
    }

    private void OnSubmit()
    {
        if (FormModel.Password != FormModel.User!.Password)
        {
            ToastService.ShowError("Пароль неверный");
            return;
        }
        PalletContext.SetPalletMan(FormModel.User!);
    }
}

public class PalletManFormModel
{
    [Required(ErrorMessage = "Пользователь обязателен для заполнения")]
    public PalletMan? User { get; set; }

    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [RegularExpression(@"\d{4}$", ErrorMessage = "Пароль должен состоять из 4 цифр")]
    public string Password { get; set; } = string.Empty;
}