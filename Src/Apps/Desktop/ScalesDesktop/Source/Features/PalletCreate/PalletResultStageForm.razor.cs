using Fluxor;
using ScalesDesktop.Source.Shared.Services.Stores;
using Ws.Desktop.Models;
using Ws.Desktop.Models.Features.Pallets.Input;
using Ws.Desktop.Models.Features.Pallets.Output;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Utils;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletResultStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IDesktopApi DesktopApi { get; set; } = default!;
    [Inject] private IState<PalletManState> PalletManState { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnCancelAction { get; set; }
    [Parameter] public EventCallback<PalletInfo> OnSubmitAction { get; set; }
    [Parameter] public EventCallback<bool> OnBlockAction { get; set; }

    private bool IsLoading { get; set; }

    private async Task CreatePallet()
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
            PalletManId = PalletManState.Value.PalletMan!.Id,
            WeightTray = FormModel.PalletWeight,
            LabelCount = (byte)FormModel.Count,
            Kneading = (ushort)FormModel.Kneading,
            ProdDt = FormModel.CreateDt ?? DateTime.Now
        };

        try
        {
            PalletInfo data = await DesktopApi.CreatePiecePallet(createDto);
            await OnSubmitAction.InvokeAsync(data);
            ToastService.ShowSuccess(Localizer["PalletCreateDialogSuccess"]);
        }
        catch (ApiException ex)
        {
            if (!ex.HasContent || string.IsNullOrEmpty(ex.Content) || !SerializationUtils.TryDeserialize(ex.Content, out ApiExceptionClient? exception) || exception == null)
                ToastService.ShowError(Localizer["ToastPalletCreateError"]);
            else
                ToastService.ShowError(Localizer[exception.LocalizeMessage]);
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