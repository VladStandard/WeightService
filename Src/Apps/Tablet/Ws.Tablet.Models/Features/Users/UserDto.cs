using Ws.Shared.Converters;

namespace Ws.Tablet.Models.Features.Users;

public sealed class UserDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public Fio Fio { get; set; } = DefaultTypes.Fio;
}