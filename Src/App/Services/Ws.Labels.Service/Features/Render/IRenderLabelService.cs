namespace Ws.Labels.Service.Features.Render;

public interface IRenderLabelService
{
    /// <summary>
    /// Преобразует ZPL в Base64 изображение.
    /// </summary>
    /// <param name="zpl">ZPL строка.</param>
    /// <returns>Base64 изображение.</returns>
    /// <exception cref="RenderLabelException">Ошибка рендера.</exception>
    Task<string> GetZplPreviewBase64(string zpl);
}