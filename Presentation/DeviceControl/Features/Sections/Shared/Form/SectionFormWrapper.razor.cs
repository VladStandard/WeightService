using Blazor.Heroicons;
using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormWrapper: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    
    [Parameter] public DateTime? CreateDate { get; set; }
    [Parameter] public DateTime? ChangeDate { get; set; }
    [Parameter] public EventCallback OnSubmitAction { get; set; }
    [Parameter] public EventCallback ResetItemAction { get; set; }
    [Parameter] public EventCallback DeleteItemAction { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public string ShareUrl { get; set; } = string.Empty;
    [Parameter] public IEnumerable<ActionMenuEntry> AdditionButtons { get; set; } = [];
    
    private IEnumerable<ActionMenuEntry> ActionMenuEntries { get; set; } = new List<ActionMenuEntry>();

    protected override void OnInitialized()
    {
        InitializeActionMenu();
    }

    private void InitializeActionMenu()
    {
        if (!string.IsNullOrEmpty(ShareUrl))
            ActionMenuEntries = ActionMenuEntries.Append(new()
                {
                    Name = Localizer["ActionMenuShare"],
                    IconName = HeroiconName.Share,
                    OnClickAction = EventCallback.Factory.Create(this, CopyToClipboard)
                });
        if (ResetItemAction.HasDelegate)
            ActionMenuEntries = ActionMenuEntries.Append(new()
                {
                    Name = Localizer["ActionMenuReset"],
                    IconName = HeroiconName.ArrowTopRightOnSquare,
                    OnClickAction = EventCallback.Factory.Create(this, ResetItemAction)
                });
        if(DeleteItemAction.HasDelegate)
            ActionMenuEntries = ActionMenuEntries.Append(new()
            {
                Name = Localizer["ActionMenuDelete"],
                IconName = HeroiconName.Trash,
                OnClickAction = EventCallback.Factory.Create(this, DeleteItemAction),
                CustomClass = "hover:bg-red-200 hover:text-red-600"
            });
        ActionMenuEntries = ActionMenuEntries.Concat(AdditionButtons);
    }

    private string GetAbsoluteUrl(string relativePath) =>
        new Uri(new(NavigationManager.BaseUri), relativePath).AbsoluteUri;

    private async Task CopyToClipboard()
    {
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", GetAbsoluteUrl(ShareUrl));
        await NotificationService.Info(Localizer["ToastUrlCopyToClipboard"]);
    }
    
}

public class ActionMenuEntry
{
    public string Name { get; init; } = string.Empty;
    public EventCallback OnClickAction { get; init; } = EventCallback.Empty;
    public string IconName { get; init; } = string.Empty;
    public string CustomClass { get; init; } = string.Empty;
}