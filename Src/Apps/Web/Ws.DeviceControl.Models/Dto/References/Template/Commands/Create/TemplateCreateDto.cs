namespace Ws.DeviceControl.Models.Dto.References.Template.Commands.Create;

public sealed record TemplateCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public short Width { get; set; }

    [JsonPropertyName("height")]
    public short Height { get; set; }

    [JsonPropertyName("rotate")]
    public short Rotate { get; set; }

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("isWeight")]
    public bool IsWeight { get; set; }
}