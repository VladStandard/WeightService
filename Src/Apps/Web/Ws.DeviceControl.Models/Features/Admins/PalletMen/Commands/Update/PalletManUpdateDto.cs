namespace Ws.DeviceControl.Models.Features.Admins.PalletMen.Commands.Update;

public sealed record PalletManUpdateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("patronymic")]
    public string Patronymic { get; set; } = string.Empty;

    [JsonPropertyName("surname")]
    public string Surname { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("warehouseId")]
    public Guid WarehouseId { get; set; }
}