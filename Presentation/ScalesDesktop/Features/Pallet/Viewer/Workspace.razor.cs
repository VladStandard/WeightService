using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Create;
using ScalesDesktop.Features.Pallet.Resources;
using ScalesDesktop.Resources;
using ScalesDesktop.Services;
using Ws.Shared.Enums;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Pallet.Viewer;

public sealed partial class Workspace : ComponentBase, IDisposable
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;
    [Inject] private PalletContext PalletContext { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    private List<EnumTypeModel<string>> TabsButtonList { get; set; } = [];
    private string SelectedTab { get; set; } = "main";
    private Tabs TabsRef { get; set; } = null!;

    protected override void OnInitialized()
    {
        TabsButtonList = [new(WsDataLocalizer["ColData"], "main"), new(WsDataLocalizer["ColLabel"], "labels")];
        PalletContext.OnStateChanged += StateHasChanged;
    }
    
    private async Task ShowCreateFormDialog() => await ModalService.Show<CreateFormModal>();

    private void CloseCurrentPallet() => PalletContext.ChangePallet(new());

    private bool IsSelectedTab(string tabName) => SelectedTab == tabName;

    private async Task ChangeSelectedTab(string tabName)
    {
        await TabsRef.SelectTab(tabName);
        StateHasChanged();
    }

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}