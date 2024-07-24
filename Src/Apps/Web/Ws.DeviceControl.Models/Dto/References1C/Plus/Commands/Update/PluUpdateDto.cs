namespace Ws.DeviceControl.Models.Dto.References1C.Plus.Commands.Update;

public class PluUpdateDto
{
    [JsonPropertyName("templateId")]
    public Guid TemplateId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("fullName")]
    public string FullName { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}