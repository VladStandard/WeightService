namespace Ws.Labels.Service.Features.PrintLabel.Models;

internal record ZplPrintItems
{
    public required string Template;
    public required string StorageMethod;
    public required Dictionary<string, string> Resources;
};