using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.LabelsService.Features.RenderLabel;
using Ws.LabelsService.Features.RenderLabel.Exceptions;

namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsPreview
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IRenderLabelService RenderLabelService { get; set; } = null!;
    
    [Parameter, EditorRequired] public string ZplCode { get; set; } = string.Empty;
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
            await NotificationService.Error(Localizer["LabelsPreviewErrorMsg"]);
        }
    }
    
    private string GetImageData() => $"data:image/png;base64,{ImageData}";
}