using Ws.Shared.Constants;
using Ws.Shared.ValueTypes;

namespace Ws.Tablet.Models.Features.Users;

[Serializable]
public class UserDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public Fio Fio { get; set; } = DefaultTypes.Fio;
}