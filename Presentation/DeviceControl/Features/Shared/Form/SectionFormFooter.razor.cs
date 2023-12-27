using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormFooter: ComponentBase
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    [Parameter] public DateTime? CreateDate { get; set; }
    [Parameter] public DateTime? ChangeDate { get; set; }
    [Parameter] public EventCallback OnSaveAction { get; set; }
    [Parameter] public string ShareUrl { get; set; } = string.Empty;

    protected override void OnParametersSet()
    {
        if (string.IsNullOrEmpty(ShareUrl)) return;
        Uri baseUri = new(NavigationManager.BaseUri);
        ShareUrl = new Uri(baseUri, ShareUrl).AbsoluteUri;
    }

    private async Task CloseModal() => await ModalService.Hide();

    private async Task CopyToClipboard()
        => await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", ShareUrl);
    
    private async Task SaveAction()
    {
        await OnSaveAction.InvokeAsync();
        await CloseModal();
    }
}