using Ws.Labels.Service.Generate.Common.BarcodeLabel;
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
}

file class BarcodeWeightTemp : IBarcodeWeightLabel
{
    public decimal Weight { get; init; }
}

file class BarcodePieceTemp : IBarcodePieceLabel
{
    public short BundleCount { get; init; }
}

#endregion

public static class TemplateTypesUtils
{
    #region Public

    public static List<BarcodeVariable> GetVarsForPieceTemplate()
    {
        BarcodePieceTemp data = new();

        List<BarcodeVariable> vars = GetBaseVariable();
        vars.Add(new(() => data.BundleCount, 2));

        return vars.OrderBy(i => i.Name).ToList();
    }

    public static List<BarcodeVariable> GetVarForWeightTemplate()
    {
        BarcodeWeightTemp data = new();

        List<BarcodeVariable> vars = GetBaseVariable();
        vars.Add(new(() => data.Weight, 5));

        return vars.OrderBy(i => i.Name).ToList();
    }

    # endregion

    #region Private

    private static List<BarcodeVariable> GetBaseVariable()
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
        ];
    }

    #endregion
}