namespace Ws.DeviceControl.Models.Dto.References.Template.Commands.Create;

public sealed record TemplateCreateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public required ushort Width { get; set; }

    [JsonPropertyName("height")]
    public required ushort Height { get; set; }

    [JsonPropertyName("rotate")]
    public required ushort Rotate { get; set; }

    [JsonPropertyName("body")]
    public required string Body { get; set; }

    [JsonPropertyName("isWeight")]
    public required bool isWeight { get; set; }
}