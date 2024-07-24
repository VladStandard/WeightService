using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Shared.Api.ValueTypes;

namespace Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;

public class PalletManDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("id1C")]
    public required Guid Id1C { get; init; }

    [JsonPropertyName("fio")]
    public required Fio Fio { get; init; }

    [JsonPropertyName("password")]
    public required string Password { get; init; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; init; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; init; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; init; }
}