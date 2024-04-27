using Ws.Labels.Service.Features.Generate.Common;
using Ws.Labels.Service.Features.Generate.Models;

namespace Ws.Labels.Service.Features.Generate.Utils;

public static partial class TemplateTypesUtils
{
    public static List<IBarcodeFieldModel> GetVariablesForPieceTemplate()
    {
        BarcodePieceTemp data = new();
        List<IBarcodeFieldModel> vars = GetBaseVariable();
        vars.Add(new BarcodeFieldModel<short>(data.BundleCount, 2));
        return vars;
    }

    public static List<IBarcodeFieldModel> GetVariablesForWeightTemplate()
    {
        BarcodeWeightTemp data = new();
        List<IBarcodeFieldModel> vars = GetBaseVariable();
        vars.Add(new BarcodeFieldModel<decimal>(data.Weight, 5));
        return vars;
    }
}