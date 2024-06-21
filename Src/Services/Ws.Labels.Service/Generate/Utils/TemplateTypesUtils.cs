using Ws.Labels.Service.Generate.Models;

namespace Ws.Labels.Service.Generate.Utils;

public static partial class TemplateTypesUtils
{
    public static List<BarcodeVariable> GetVariablesForPieceTemplate()
    {
        BarcodePieceTemp data = new();
        List<BarcodeVariable> vars = GetBaseVariable();
        vars.Add(BarcodeVariable.Build(() => data.BundleCount, 2));
        return vars.OrderBy(i => i.Name).ToList();
    }

    public static List<BarcodeVariable> GetVariablesForWeightTemplate()
    {
        BarcodeWeightTemp data = new();
        List<BarcodeVariable> vars = GetBaseVariable();
        vars.Add(BarcodeVariable.Build(() => data.Weight, 5));
        return vars.OrderBy(i => i.Name).ToList();
    }
}