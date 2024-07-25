using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models.Dto.Print.Labels;

public record LabelDto
{
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    [JsonPropertyName("isWeight")]
    public required bool IsWeight { get; set; }

    #region Proxies

    [JsonPropertyName("arm")]
    public required ProxyDto Arm { get; set; }

    [JsonPropertyName("warehouse")]
    public required ProxyDto Warehouse { get; set; }

    [JsonPropertyName("productionSite")]
    public required ProxyDto ProductionSite { get; set; }

    [JsonPropertyName("plu")]
    public required ProxyDto? Plu { get; set; }

    [JsonPropertyName("pallet")]
    public required ProxyDto? Pallet { get; set; }

    #endregion

    #region Barcodes

    [JsonPropertyName("barcodeTop")]
    public required string BarcodeTop { get; set; }

    [JsonPropertyName("barcodeBottom")]
    public required string BarcodeBottom { get; set; }

    [JsonPropertyName("barcodeRight")]
    public required string BarcodeRight { get; set; }

    #endregion

    #region Dates

    [JsonPropertyName("productDt")]
    public required DateOnly ProductDt { get; set; }

    [JsonPropertyName("createDt")]
    public required DateOnly ExpirationDt { get; set; }

    [JsonPropertyName("createDt")]
    public required DateTime CreateDt { get; set; }

    #endregion
}