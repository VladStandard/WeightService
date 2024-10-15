using Ws.Shared.Constants;
using Ws.Shared.ValueTypes;

namespace Ws.Tablet.Models.Features.Users;

[Serializable]
public class UserDto
{
    [JsonConverter(typeof(FioJsonConverter))]
    private Fio Fio { get; set; } = DefaultTypes.Fio;
}