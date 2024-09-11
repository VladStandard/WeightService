using Ws.PalychExchange.Api.App.Features.Characteristics.Common;
using Ws.PalychExchange.Api.App.Features.Characteristics.Dto;
using Ws.PalychExchange.Api.App.Features.Characteristics.Services.Models;

namespace Ws.PalychExchange.Api.App.Features.Characteristics.Services;

internal sealed partial class CharacteristicApiService(GroupedCharacteristicValidator validator) :
    BaseService<GroupedCharacteristic>(validator), ICharacteristicService
{
    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper)
    {
        List<GroupedCharacteristic> grouped = dtoWrapper.ToGrouped();
        ResolveUniqueUidLocal(grouped);

        DeleteCharacteristics(grouped);

        ResolveUniqueLocal(
            grouped,
            dto => (dto.PluUid, dto.BoxUid, dto.BundleCount),
            "Характеристика - (Box, Plu, BundleCount) не уникальна"
        );

        List<GroupedCharacteristic> validDtos = FilterValidDtos(grouped);

        ResolveUniqueDb(validDtos);
        ResolveIsWeightDb(validDtos);

        ResolveNotExistsFkDb(validDtos, DbContext.Plus, dto => dto.PluUid, "Плу - не найдена");
        ResolveNotExistsFkDb(validDtos, DbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");

        SaveCharacteristics(validDtos);
        return OutputDto;
    }
}