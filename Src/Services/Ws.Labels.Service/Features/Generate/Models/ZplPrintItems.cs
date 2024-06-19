namespace Ws.Labels.Service.Features.Generate.Models;

internal record ZplPrintItems
{
    public required string Template;
    public required string StorageMethod;
    public required Dictionary<string, string> Resources;
};