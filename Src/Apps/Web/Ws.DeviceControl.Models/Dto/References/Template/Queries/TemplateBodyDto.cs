namespace Ws.DeviceControl.Models.Dto.References.Template.Queries;

public class TemplateBodyDto
{
    [JsonPropertyName("body")]
    public required string Body { get; set; } = string.Empty;
}