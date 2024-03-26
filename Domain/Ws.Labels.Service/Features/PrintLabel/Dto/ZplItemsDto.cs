namespace Ws.Labels.Service.Features.PrintLabel.Dto;

internal record ZplItemsDto
{
    public required string Template;
    public required string StorageMethod;
    public required Dictionary<string, string> Resources;
};