using Ws.Labels.Service.Features.Render.Exceptions;

namespace Ws.Labels.Service.Features.Render;

public interface IRenderLabelService
{
    /// <summary>
    /// Преобразует ZPL в Base64 изображение.
    /// </summary>
    /// <param name="zpl"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns>Base64 изображение.</returns>
    /// <exception cref="RenderLabelException">Ошибка рендера.</exception>
    Task<string> GetZplPreviewBase64(string zpl, decimal width, decimal height);
}