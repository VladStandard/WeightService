using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Services;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletResultStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;
    [Inject] private ArmApi ArmApi { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnCancelAction { get; set; }
    [Parameter] public EventCallback<PalletInfo> OnSubmitAction { get; set; }

    private bool IsLoading { get; set; }

    private async Task CreatePallet(ArmValue arm)
    {
        if (IsLoading) return;
        IsLoading = true;

        PalletPieceCreateDto createDto = new()
        {
            PluId = FormModel.Plu!.Id,
            CharacteristicId = FormModel.Nesting!.Id,
            PalletManId = PalletContext.PalletMan!.Id,
            WeightTray = FormModel.PalletWeight,
            LabelCount = (byte)FormModel.Count,
            Kneading = (ushort)FormModel.Kneading,
            ProdDt = FormModel.CreateDt ?? DateTime.Now
        };

        try
        {
            PalletInfo data = await DesktopApi.CreatePiecePallet(arm.Id, createDto);
            await OnSubmitAction.InvokeAsync(data);
        }
        catch (Exception ex)
        {
            ToastService.ShowError(Localizer["ToastPalletCreateError"]);
        }

        IsLoading = false;
    }
}