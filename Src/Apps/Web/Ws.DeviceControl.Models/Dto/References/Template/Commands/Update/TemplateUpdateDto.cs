namespace Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;

public sealed record TemplateUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public required short Width { get; set; }

    [JsonPropertyName("height")]
    public required short Height { get; set; }

    [JsonPropertyName("rotate")]
    public required short Rotate { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;
}