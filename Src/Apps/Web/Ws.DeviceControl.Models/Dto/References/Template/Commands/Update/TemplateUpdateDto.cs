namespace Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;

public sealed record TemplateUpdateDto
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
    public string Body { get; set; } = string.Empty;
}