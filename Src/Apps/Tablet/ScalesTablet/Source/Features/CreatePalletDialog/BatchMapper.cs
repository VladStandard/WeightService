using ScalesTablet.Source.Shared.Utils;
using Ws.Tablet.Models.Features.Pallets.Input;

namespace ScalesTablet.Source.Features.CreatePalletDialog;

public static class BatchMapper
{
    public static BatchCreateDto ModelToCreateDto(BatchCreateModel item)
    {
        DateTimeUtil.TryParseStringDate(item.Date, out DateTime date);
        return new()
        {
            PluId = item.Plu.Id,
            Date = date,
            Weight = item.Weight
        };
    }
}