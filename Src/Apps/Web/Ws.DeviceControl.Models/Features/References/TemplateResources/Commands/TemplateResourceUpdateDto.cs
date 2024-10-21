namespace Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;

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

public sealed class TemplateResourceUpdateValidator : AbstractValidator<TemplateResourceUpdateDto>
{
    public TemplateResourceUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Body)
            .NotEmpty().MaximumLength(8000)
            .WithName(wsDataLocalizer["ColTemplate"]);
    }
}