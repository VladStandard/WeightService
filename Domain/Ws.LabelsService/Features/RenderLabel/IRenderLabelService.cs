namespace Ws.LabelsService.Features.RenderLabel;

public interface IRenderLabelService
{
    Task<string> GetZplPreviewBase64(string zpl);
}