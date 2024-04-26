using DeviceControl.Source.Shared.Localization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Labels.Service.Features.Render;
using Ws.Labels.Service.Features.Render.Exceptions;

namespace DeviceControl.Source.Pages.Operations.Labels;

public sealed partial class LabelsPreview
{
    # region Injects

    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IRenderLabelService RenderLabelService { get; set; } = default!;

    # endregion

    [Parameter, EditorRequired] public string ZplCode { set; get; } = string.Empty;
    private string ImageData { get; set; } = string.Empty;
    private bool IsLoading { get; set; } = true;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender) return;
        await GenerateZplImageAsync();
        IsLoading = false;
        StateHasChanged();
    }

    private async Task GenerateZplImageAsync()
    {
        try
        {
            ImageData = await RenderLabelService.GetZplPreviewBase64(ZplCode);
        }
        catch (RenderLabelException)
        {
            ToastService.ShowError(Localizer["LabelsPreviewErrorMsg"]);
        }
    }

    private string GetImageData() => $"data:image/png;base64,{ImageData}";
}