namespace Ws.Barcodes.Features.Barcodes.Common;

internal interface IBarcodeVariables
{
    public uint LineCounter { get; init; }
    public uint LineNumber { get; init; }

    public string PluGtin { get; init; }
    public string PluEan13 { get; init; }
    public ushort PluNumber { get; init; }

    public DateTime ProductDt { get; init; }
    public DateTime ExpirationDt { get; init; }
    public ushort ExpirationDay { get; init; }

    public decimal WeightNet { get; init; }
    public ushort BundleCount { get; init; }
    public ushort Kneading { get; init; }
}