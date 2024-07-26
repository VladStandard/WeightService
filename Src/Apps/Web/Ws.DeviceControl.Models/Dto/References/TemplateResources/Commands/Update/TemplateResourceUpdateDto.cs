namespace Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;

public sealed record TemplateResourceUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("body")]
    public required string Body { get; set; }

    [JsonPropertyName("type")]
    [JsonConverter(typeof(EnumJsonConverter<ZplResourceType>))]
    public required ZplResourceType Type { get; set; }
}