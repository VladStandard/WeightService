using Ws.Barcodes.Formatters;
using Ws.Barcodes.Models;
using Ws.Barcodes.Utils;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public static class BarcodeMapper
{
    public static BarcodeItemDto ExtendedDtoToDto(ExtendedBarcodeItemDto item) => item;

    public static ExtendedBarcodeItemDto DtoToExtendedDto(BarcodeItemDto item, List<BarcodeVarInfo> vars) =>
        new()
        {
            Property = item.Property,
            FormatStr = item.FormatStr,
            Example = GetBarcodeExample(item, vars),
            IsConst = vars.FirstOrDefault(x => x.Property == item.Property) == null
        };

    public static string TryGetFormatedValue(string mask, object value)
    {
        try
        {
            return string.Format(BarcodeFormatter.Default, mask, value);
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GetBarcodeExample(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        BarcodeVarInfo? typedVariable = vars.FirstOrDefault(x => x.Property == item.Property);
        return BarcodeRegexUtils.GetFriendlyChars(
            TryGetFormatedValue(item.FormatStr, typedVariable == null ? item.Property : typedVariable.Example)
            );
    }
}