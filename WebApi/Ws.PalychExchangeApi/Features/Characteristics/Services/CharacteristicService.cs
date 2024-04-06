using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Common;
using Ws.PalychExchangeApi.Features.Characteristics.Dto.PluCharacteristicsWrapper;
using Ws.PalychExchangeApi.Features.Characteristics.Services.Models;

namespace Ws.PalychExchangeApi.Features.Characteristics.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal sealed partial class CharacteristicService(WsDbContext dbContext, GroupedCharacteristicValidator validator) :
    BaseService<GroupedCharacteristic>(dbContext, validator), ICharacteristicService
{
    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper)
    {
        List<GroupedCharacteristic> grouped = dtoWrapper.ToGrouped();

        ResolveUniqueUidLocal(grouped);

        ResolveUniqueLocal(
            grouped,
            dto => (dto.PluUid, dto.BoxUid, dto.BundleCount),
            "Характеристика - (Box, Plu, BundleCount) не уникальна"
        );

        IEnumerable<GroupedCharacteristic> validDtos = FilterValidDtos(grouped);

        ResolveUniqueDb(grouped);

        ResolveNotExistsFkDb(grouped, dbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");

        SaveCharacteristics(validDtos);
        return OutputDto;
    }
}