using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public static class BarcodeMapper
{
    public static BarcodeItemDto ExtendedDtoToDto(ExtendedBarcodeItemDto item)
    {
        return new()
        {
            Property = item.Property,
            FormatStr = item.FormatStr
        };
    }

    public static ExtendedBarcodeItemDto DtoToExtendedDto(BarcodeItemDto item, BarcodeVarDto[] vars)
    {
        BarcodeVarDto? typedVariable = vars.FirstOrDefault(x => x.Name == item.Property);
        ushort varLength = typedVariable == null
            ? (ushort)item.Property.Length
            : typedVariable.Type == typeof(DateTime)
                ? (ushort)item.FormatStr.Length
                : (ushort)typedVariable.Length;
        return new()
        {
            Property = item.Property,
            FormatStr = item.FormatStr,
            Length = varLength,
            IsConst = typedVariable == null
        };
    }
}