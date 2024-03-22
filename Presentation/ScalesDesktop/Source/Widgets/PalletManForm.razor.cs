using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    [Inject] private IToastService ToastService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IPalletManService PalletManService { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [SupplyParameterFromForm] private PalletManFormModel FormModel { get; set; } = new();
    private IEnumerable<PalletManEntity> GetAllPalletMen { get; set; } = [];

    protected override void OnInitialized()
    {
        GetAllPalletMen = PalletManService.GetAll();
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
    public PalletManEntity? User { get; set; }

    [Required(ErrorMessage = "Пароль обязателен для заполнения")]
    [RegularExpression(@"\d{4}$", ErrorMessage = "Пароль должен состоять из 4 цифр")]
    public string Password { get; set; } = string.Empty;
}