namespace Ws.Labels.Service.Features.RenderLabel;

public interface IRenderLabelService
{
    Task<string> GetZplPreviewBase64(string zpl);
}