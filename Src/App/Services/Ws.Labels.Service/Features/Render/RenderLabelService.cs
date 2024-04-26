using System.Net.Http.Headers;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;
using Ws.Labels.Service.Features.Render.Exceptions;

namespace Ws.Labels.Service.Features.Render;

internal class RenderLabelService : IRenderLabelService
{
    public async Task<string> GetZplPreviewBase64(string zpl)
    {
        const string requestUrl = "http://api.labelary.com/v1/printers/12dpmm/labels/2.36x5.9/0/";
        byte[] zplBytes = Encoding.UTF8.GetBytes(zpl);

        using HttpClient client = new();
        using HttpContent content = new ByteArrayContent(zplBytes);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        try
        {
            HttpResponseMessage response = await client.PostAsync(requestUrl, content);
            if (!response.IsSuccessStatusCode) throw new RenderLabelException();

            byte[] imageBytes = await GetImageBytesFromResponse(response);
            return ConvertImageToBase64(imageBytes);
        }
        catch (Exception)
        {
            throw new RenderLabelException();
        }
    }

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
        using Image image = Image.Load(imageStream);

        image.Mutate(x => x.Rotate(90));

        using MemoryStream rotatedStream = new();
        image.Save(rotatedStream, new PngEncoder());
        return Convert.ToBase64String(rotatedStream.ToArray());
    }

    #endregion
}