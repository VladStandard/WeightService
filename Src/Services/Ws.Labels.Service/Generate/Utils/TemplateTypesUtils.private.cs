using Ws.Labels.Service.Generate.Common.BarcodeLabel;
using Ws.Labels.Service.Generate.Common.XmlBarcode;
using Ws.Labels.Service.Generate.Models;

namespace Ws.Labels.Service.Generate.Utils;

public partial class TemplateTypesUtils
{
    private class BarcodeLabelBaseTemp : IBarcodeLabel
    {
        public int LineNumber { get; set; }
        public int LineCounter { get; set; }
        public short Kneading { get; set; }
        public short PluNumber { get; set; }
        public string PluGtin { get; set; } = null!;
        public DateTime ProductDt { get; set; }
    }

    private class BarcodeWeightTemp : IBarcodeWeightLabel
    {
        public decimal Weight { get; set; }
    }

    private class BarcodePieceTemp : IBarcodePieceLabel
    {
        public short BundleCount { get; set; }
    }

    private static List<BarcodeVariable> GetBaseVariable()
    {
        BarcodeLabelBaseTemp data = new();
        return [
            BarcodeVariable.Build( () => data.LineNumber,5),
            BarcodeVariable.Build( () => data.LineCounter,7),
            BarcodeVariable.Build( () => data.PluNumber,3),
            BarcodeVariable.Build( () => data.PluGtin,14),
            BarcodeVariable.Build( () => data.Kneading,3),
            BarcodeVariable.Build( () => data.ProductDt,0, true),
        ];
    }
}