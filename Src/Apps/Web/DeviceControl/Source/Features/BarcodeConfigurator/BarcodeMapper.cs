using Ws.Barcodes.Formatters;
using Ws.Barcodes.Models;
using Ws.Barcodes.Utils;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public static class BarcodeMapper
{
    public static BarcodeItemDto ExtendedDtoToDto(ExtendedBarcodeItemDto item) => item;

    public static BarcodeVar ItemDtoToVar(BarcodeItemDto item) => new(item.Property, item.FormatStr);

    public static ExtendedBarcodeItemDto DtoToExtendedDto(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        bool isConst = vars.FirstOrDefault(x => x.Property == item.Property) == null;
        return new()
        {
            Property = item.Property,
            FormatStr = item.FormatStr,
            CachedMask = item.FormatStr,
            Example = GetBarcodeExample(item, vars),
            IsConst = isConst,
            DefaultLength = isConst ? -1 : GetDefaultMaskLength(item, vars)
        };
    }

    public static int GetDefaultMaskLength(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        BarcodeVarInfo? barcode = GetTypedVariable(item, vars);
        if (barcode == null || barcode.Type == typeof(DateTime)) return -1;
        string defaultExample = TryGetFormatedValue(barcode.Mask, barcode.Example);
        return string.IsNullOrEmpty(defaultExample) ? -1 : defaultExample.Length;
    }

    public static string GetBarcodeExample(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        BarcodeVarInfo? typedVariable = GetTypedVariable(item, vars);
        object objectToFormat = typedVariable == null ? item.Property : typedVariable.Example;
        return BarcodeRegexUtils.GetFriendlyChars(TryGetFormatedValue(item.FormatStr, objectToFormat));
    }

    private static BarcodeVarInfo? GetTypedVariable(BarcodeItemDto item, List<BarcodeVarInfo> vars) =>
        vars.FirstOrDefault(x => x.Property == item.Property);

    private static string TryGetFormatedValue(string mask, object value)
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
}