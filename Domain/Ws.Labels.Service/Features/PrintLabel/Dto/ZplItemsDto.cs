namespace Ws.Labels.Service.Features.PrintLabel.Dto;

internal record ZplItemsDto(string Template, string StorageMethod, Dictionary<string, string> Resources);