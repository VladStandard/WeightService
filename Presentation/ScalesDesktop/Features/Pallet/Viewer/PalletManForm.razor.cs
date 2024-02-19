using System.ComponentModel.DataAnnotations;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Resources;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.PalletMan;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Pallet.Viewer;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed partial class PalletManForm : ComponentBase
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private IPalletManService PalletManService { get; set; } = null!;
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    
    [SupplyParameterFromForm] private PalletManFormModel FormModel { get; set; } = new();
    
    private IEnumerable<PalletManEntity> GetAllPalletMen() => PalletManService.GetAll();
    
    private void HandleInvalidForm(EditContext context)
    {
        foreach (string msg in context.GetValidationMessages())
            NotificationService.Error(msg);
    }

    private void OnSubmit()
    {
        if (FormModel.Password != FormModel.User!.Password)
        {
            NotificationService.Error("Пароль неверный");
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