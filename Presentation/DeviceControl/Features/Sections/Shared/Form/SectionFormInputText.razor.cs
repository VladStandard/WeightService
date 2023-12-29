using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;

namespace DeviceControl.Features.Sections.Shared.Form;

public sealed partial class SectionFormInputText: SectionFormInputBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public bool IsDisabled { get; set; }
    [Parameter] public string Value { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> ValueChanged { get; set; }
    [Parameter] public SectionFormInputSizeEnum Size { get; set; } = SectionFormInputSizeEnum.Small;
    [Parameter] public bool IsCopyable { get; set; }

    private async Task HandleValueChange(string arg)
    {
        Value = arg;
        await ValueChanged.InvokeAsync(Value);
    }

    private bool GetIsLargeSize(SectionFormInputSizeEnum size) =>
        size is SectionFormInputSizeEnum.Large or SectionFormInputSizeEnum.ExtraLarge;

    private async Task SaveToClipboard(string value) =>
        await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", value);
}