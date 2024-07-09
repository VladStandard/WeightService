using Ws.Labels.Service.Generate.Common;
using Ws.Labels.Service.Generate.Models;

namespace Ws.Labels.Service.Generate.Utils;

#region File scope

file class BarcodeLabelBaseTemp : IBarcodeLabel
{
    public int LineNumber { get; init; }
    public int LineCounter { get; init; }
    public short Kneading { get; init; }
    public short PluNumber { get; init; }
    public string PluEan13 { get; init; } = null!;
    public string PluGtin { get; init; } = null!;
    public DateTime ProductDt { get; init; }
    public DateTime ExpirationDt { get; init; }
    public short BundleCount { get; init; }
    public decimal WeightNet { get; init; }
}

#endregion

public static class TemplateTypesUtils
{
    public static List<BarcodeVariable> GetVariables()
    {
        BarcodeLabelBaseTemp data = new();
        return [
            new(() => data.LineNumber,5),
            new(() => data.LineCounter,6),
            new(() => data.PluNumber,3),
            new(() => data.PluGtin,14),
            new(() => data.PluEan13,13),
            new(() => data.Kneading,3),
            new(() => data.ProductDt,0, true),
            new(() => data.ExpirationDt,0, true),
            new(() => data.WeightNet,5),
            new(() => data.BundleCount,2)
        ];
    }
}