using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Domain.Services.Features.Plu;
using Ws.Labels.Service.Features.PrintLabel;
using Ws.Labels.Service.Features.PrintLabel.Features.Piece.Dto;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Features.PalletCreate;

public sealed partial class PalletResultStageForm : ComponentBase
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IPrintLabelService PrintLabelService { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private IPluService PluService { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public PalletCreateModel FormModel { get; set; } = default!;
    [Parameter] public EventCallback OnCancelAction { get; set; }
    [Parameter] public EventCallback OnSubmitAction { get; set; }

    private bool IsLoading { get; set; }

    private async Task CreatePallet()
    {
        if (IsLoading) return;
        IsLoading = true;

        DateTime createDt = FormModel.CreateDt ?? DateTime.Now;
        GeneratePiecePalletDto dto = new()
        {
            PalletMan = PalletContext.PalletMan,
            Weight = FormModel.PalletWeight,
            ExpirationDt = createDt.AddDays(FormModel.Plu!.ShelfLifeDays),
            Kneading = FormModel.Kneading,
            Line = LineContext.Line,
            Characteristic = FormModel.Nesting!,
            ProductDt = createDt,
            Plu = FormModel.Plu,
        };

        try
        {
            await Task.Run(() => { PrintLabelService.GeneratePiecePallet(dto, FormModel.Count); });
            PalletContext.UpdatePalletData();
            await OnSubmitAction.InvokeAsync();
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastPalletCreateError"]);
        }

        IsLoading = false;
    }
}