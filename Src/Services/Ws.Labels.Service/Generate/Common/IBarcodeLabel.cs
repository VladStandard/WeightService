namespace Ws.Labels.Service.Generate.Common;

internal interface IBarcodeLabel
{
    public int LineNumber { get; init; }
    public int LineCounter { get; init; }
    public short Kneading { get; init; }
    public short PluNumber { get; init; }
    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public DateTime ProductDt { get; init; }
    public DateTime ExpirationDt { get; init; }
    public short BundleCount { get; init; }
    public decimal WeightNet { get; init; }
}