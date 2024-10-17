using Ws.PalychExchange.Api.App.Features.Characteristics.Common;
using Ws.PalychExchange.Api.App.Features.Characteristics.Impl.Models;
using GroupedCharacteristicValidator=Ws.PalychExchange.Api.App.Features.Characteristics.Impl.Models.GroupedCharacteristicValidator;

namespace Ws.PalychExchange.Api.App.Features.Characteristics.Impl;

internal sealed partial class CharacteristicApiService(GroupedCharacteristicValidator validator, ILogger<CharacteristicApiService> logger) :
    BaseService<GroupedCharacteristic>(validator), ICharacteristicService
{
    public ResponseDto Load(HashSet<GroupedCharacteristic> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        DeleteCharacteristics(dtos);

        ResolveUniqueLocal(
            dtos,
            dto => (dto.PluUid, dto.BoxUid, dto.BundleCount),
            "Характеристика - (Box, Plu, BundleCount) не уникальна"
        );

        FilterValidDtos(dtos);

        ResolveUniqueDb(dtos);
        ResolveIsWeightDb(dtos);

        ResolveNotExistsFkDb(dtos, DbContext.Plus, dto => dto.PluUid, "Плу - не найдена");
        ResolveNotExistsFkDb(dtos, DbContext.Boxes, dto => dto.BoxUid, "Коробка - не найдена");

        SaveCharacteristics(dtos);
        return OutputDto;
    }
}