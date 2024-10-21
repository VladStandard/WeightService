using Ws.Shared.Converters.Json;

namespace Ws.Mobile.Models.Features.Users;

public sealed class UserDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("fio")]
    [JsonConverter(typeof(FioJsonConverter))]
    public Fio Fio { get; set; } = DefaultTypes.Fio;

    [JsonPropertyName("warehouseName")]
    public required string WarehouseName { get; set; } = string.Empty;
}