namespace Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Create;

public sealed record TemplateResourceCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public string Body { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ZplResourceType>))]
    public ZplResourceType Type { get; set; }
}