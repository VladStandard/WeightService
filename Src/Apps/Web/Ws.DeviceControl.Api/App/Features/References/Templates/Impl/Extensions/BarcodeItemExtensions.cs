using Ws.DeviceControl.Api.App.Shared.Utils;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
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
        List<BarcodeVarDto> variables = BarcodeUtils.GetVariables();

        return item.Select(i =>
        {
            BarcodeVarDto? variable = variables.FirstOrDefault(j => j.Name == i.Property);
            return new BarcodeItem
            {
                Property = i.Property,
                FormatStr = i.FormatStr,
                IsConst = variable == null,
                Length = (ushort)(variable?.Length ?? 0)
            };
        }).ToList();
    }

}