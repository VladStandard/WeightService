namespace Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;

public sealed record PluDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("number")]
    public required ushort Number { get; set; }

    [JsonPropertyName("type")]
    public required bool IsWeight { get; set; }

    [JsonPropertyName("weight")]
    public required decimal Weight { get; set; }

    [JsonPropertyName("brand")]
    public required ProxyDto Brand { get; set; }

    [JsonPropertyName("shelfLifeDays")]
    public required ushort ShelfLifeDays { get; set; }

    [JsonPropertyName("template")]
    public required ProxyDto? Template { get; set; }

    [JsonPropertyName("storageMethods")]
    public required string StorageMethod { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("fullName")]
    public required string FullName { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("ean13")]
    public required string Ean13 { get; set; }

    [JsonPropertyName("gtin")]
    public required string Gtin { get; set; }

    [JsonPropertyName("clip")]
    public required ProxyDto? Clip { get; set; }

    [JsonPropertyName("bundle")]
    public required ProxyDto? Bundle { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    [JsonPropertyName("changeDt")]
    public required DateTime ChangeDt { get; set; }
}