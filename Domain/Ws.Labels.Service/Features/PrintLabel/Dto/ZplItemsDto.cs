namespace Ws.Labels.Service.Features.PrintLabel.Dto;

public record ZplItemsDto(string Template, string StorageMethod, Dictionary<string, string> Resources);