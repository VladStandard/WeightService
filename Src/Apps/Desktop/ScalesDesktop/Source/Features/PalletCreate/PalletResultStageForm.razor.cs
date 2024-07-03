using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Refit;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Shared.Utils;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Arms.Output;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Desktop.Models.Shared;

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
    [Parameter] public EventCallback<bool> OnBlockAction { get; set; }

    private bool IsLoading { get; set; }

    private async Task CreatePallet(ArmValue arm)
    {
        if (IsLoading) return;
        IsLoading = true;
        await OnBlockAction.InvokeAsync(true);

        string toastUid = Guid.NewGuid().ToString();
        ToastService.ShowProgressToast(new()
        {
          Id = toastUid,
          Intent = ToastIntent.Progress,
          Title = Localizer["PalletCreateDialogProgressToastTitle"],
          Timeout = null,
          Content = new() { Details = Localizer["PalletCreateDialogProgressToastDescription"] }
        });

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
            ToastService.ShowSuccess(Localizer["PalletCreateDialogSuccess"]);
        }
        catch (ApiException ex)
        {
            if (!ex.HasContent || string.IsNullOrEmpty(ex.Content) || !SerializationUtils.TryDeserialize(ex.Content, out ServerException? exception) || exception == null)
                ToastService.ShowError(Localizer["ToastPalletCreateError"]);
            else
                ToastService.ShowError(Localizer[exception.MessageLocalizeKey]);
        }
        catch (Exception)
        {
            ToastService.ShowError(Localizer["ToastPalletCreateError"]);
        }

        ToastService.CloseToast(toastUid);

        IsLoading = false;
        await OnBlockAction.InvokeAsync(false);
    }
}