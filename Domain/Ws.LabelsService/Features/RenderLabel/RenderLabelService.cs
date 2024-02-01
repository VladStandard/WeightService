using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using Ws.LabelsService.Features.RenderLabel.Exceptions;

namespace Ws.LabelsService.Features.RenderLabel;

public class RenderLabelService : IRenderLabelService
{
    #region Private

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

    #endregion

    public async Task<string> GetZplPreviewBase64(string zpl)
    {
        const string requestUrl = "http://api.labelary.com/v1/printers/12dpmm/labels/2.36x5.9/0/";
        byte[] zplBytes = Encoding.UTF8.GetBytes(zpl);

        using HttpClient client = new();
        using HttpContent content = new ByteArrayContent(zplBytes);
        content.Headers.ContentType = new("application/x-www-form-urlencoded");
        
        try
        {
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            if (!response.IsSuccessStatusCode) throw new RenderLabelException();
            
            byte[] imageBytes = await GetImageBytesFromResponse(response);
            return ConvertImageToBase64(imageBytes);
        }
        catch (Exception ex) when (ex is HttpRequestException or ObjectDisposedException or ArgumentException or IOException)
        {
            throw new RenderLabelException();
        }
    }
}