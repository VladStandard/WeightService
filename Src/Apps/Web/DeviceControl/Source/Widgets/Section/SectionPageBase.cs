using Microsoft.JSInterop;
using Phetch.Core;

namespace DeviceControl.Source.Widgets.Section;

public class SectionPageBase<TItem>: ComponentBase
{
    # region Injects

    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;

    # endregion

    [Parameter, SupplyParameterFromQuery] public Guid? Id { get; set; }

    protected QueryOptions DefaultEndpointOptions { get; } =
        new() { RefetchInterval = TimeSpan.FromMinutes(1), StaleTime = TimeSpan.FromMinutes(1) };
    private DialogParameters DialogParameters { get; set; } = new();

    protected override void OnInitialized()
    {
        DialogParameters = new()
        {
            OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogClosing", instance.Id)
            ),
            OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                await JsRuntime.InvokeVoidAsync("animateDialogOpening", instance.Id)
            )
        };
    }

    protected override async Task OnInitializedAsync()
    {
        if (Id == null) return;
        try
        {
            TItem item = await SearchByUidAction(Id.Value);
            await OpenSectionViewModal(item);
        }
        catch
        {
            ToastService.ShowError(Localizer["ToastErrorGettingData"]);
        }
    }

    protected async Task OpenSectionModal<T>(TItem sectionEntity) where T : SectionDialogBase<TItem> =>
        await DialogService.ShowDialogAsync<T>(new SectionDialogContent<TItem> { Item = sectionEntity },
            DialogParameters);

    protected async Task ContextFuncWrapper(TItem? item, EventCallback onComplete, Func<TItem, Task> action)
    {
        await onComplete.InvokeAsync();
        if (item == null) return;
        await action(item);
    }

    protected async Task OpenLinkInNewTab(string url) =>
        await JsRuntime.InvokeVoidAsync("open", url, "_blank");

    protected virtual Task OpenSectionViewModal(TItem item) =>
        throw new NotImplementedException();

    protected virtual Task OpenSectionCreateForm() =>
        throw new NotImplementedException();

    protected virtual Task<TItem> SearchByUidAction(Guid uid) =>
        throw new NotImplementedException();

    protected virtual Task OpenItemInNewTab(TItem item) =>
        throw new NotImplementedException();
}