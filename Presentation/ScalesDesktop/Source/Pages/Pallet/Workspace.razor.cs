using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using Ws.Shared.Enums;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Pages.Pallet;

public sealed partial class Workspace : ComponentBase, IDisposable
{
    # region Injects

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private PalletContext PalletContext { get; set; } = default!;
    [Inject] private LineContext LineContext { get; set; } = default!;

    # endregion

    private List<EnumTypeModel<string>> TabsButtonList { get; set; } = [];
    private string CurrentTabId { get; set; } = "overview";

    protected override void OnInitialized()
    {
        TabsButtonList = [new(WsDataLocalizer["ColData"], "overview"), new(WsDataLocalizer["ColLabel"], "labels")];
        PalletContext.OnStateChanged += StateHasChanged;
    }

    private void CloseCurrentPallet() => PalletContext.ChangePallet(new());

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}