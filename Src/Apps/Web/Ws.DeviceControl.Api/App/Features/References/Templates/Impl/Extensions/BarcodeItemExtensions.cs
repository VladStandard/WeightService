using Ws.DeviceControl.Models.Features.References.Template.Universal;
using Ws.Shared.ValueTypes;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;

internal static class BarcodeItemExtensions
{
    public static List<BarcodeItemDto> ToDto(this List<BarcodeItem> item)
    {
        return item.Select(i => new BarcodeItemDto
        {
            Property = i.Property,
            FormatStr = i.FormatStr,
        }).ToList();
    }

    public static List<BarcodeItem> ToItem(this List<BarcodeItemDto> item)
    {
        return item.Select(i => new BarcodeItem
        {
            Property = i.Property,
            FormatStr = i.FormatStr
        }).ToList();
    }

}