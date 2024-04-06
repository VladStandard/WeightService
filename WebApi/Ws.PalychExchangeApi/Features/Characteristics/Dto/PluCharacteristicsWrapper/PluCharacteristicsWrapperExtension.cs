using Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

namespace Ws.PalychExchangeApi.Features.Characteristics.Dto.PluCharacteristicsWrapper;

internal static class PluCharacteristicsWrapperExtensions
{
    internal static List<GroupedCharacteristic> ToGrouped(this PluCharacteristicsWrapper dto)
    {
        return dto.PluCharacteristics.SelectMany(plu => plu.Characteristics
            .GroupBy(characteristic => new GroupedCharacteristic
            {
                PluUid = plu.Uid,
                IsDelete = characteristic.IsDelete,
                Name = characteristic.Name,
                BoxUid = characteristic.BoxUid,
                Uid = characteristic.Uid,
                BundleCount = characteristic.BundleCount
            })
            .Select(group => group.Key))
        .ToList();
    }
}