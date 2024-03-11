using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Widgets.PalletCreateForm;
using Ws.Shared.Enums;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Pages.Pallet;

public sealed partial class Workspace : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;

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