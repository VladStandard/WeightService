using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Text;
using Blazorise;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Image = System.Drawing.Image;


namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsPreview
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
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
        byte[] zpl = Encoding.UTF8.GetBytes(ZplCode);

        using HttpClient client = new();
        using HttpContent content = new ByteArrayContent(zpl);
        content.Headers.ContentType = new("application/x-www-form-urlencoded");

        try
        {
            HttpResponseMessage response = await client.PostAsync(
                "http://api.labelary.com/v1/printers/12dpmm/labels/2.36x5.9/0/", content);
            if (!response.IsSuccessStatusCode) return;
            
            byte[] imageBytes = await GetImageBytesFromResponse(response);
            ImageData = ConvertImageToBase64(imageBytes);
        }
        catch (Exception ex) when (ex is HttpRequestException or ObjectDisposedException or ArgumentException or IOException)
        {
            await NotificationService.Error(Localizer["LabelsPreviewErrorMsg"]);
        }
    }

    private static async Task<byte[]> GetImageBytesFromResponse(HttpResponseMessage response)
    {
        using MemoryStream ms = new();
        await response.Content.CopyToAsync(ms);
        return ms.ToArray();
    }

    private static string ConvertImageToBase64(byte[] imageBytes)
    {
        using MemoryStream imageStream = new(imageBytes);
        using Image originalImage = Image.FromStream(imageStream);
        originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
        
        using MemoryStream rotatedStream = new();
        originalImage.Save(rotatedStream, ImageFormat.Png);
        return Convert.ToBase64String(rotatedStream.ToArray());
    }

    private string GetImageData() => $"data:image/png;base64,{ImageData}";
}