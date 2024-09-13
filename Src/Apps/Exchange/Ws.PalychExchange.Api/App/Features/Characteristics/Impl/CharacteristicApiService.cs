using Ws.PalychExchange.Api.App.Features.Characteristics.Common;
using Ws.PalychExchange.Api.App.Features.Characteristics.Impl.Models;
using Ws.PalychExchange.Api.App.Features.Characteristics.Services.Models;

namespace Ws.PalychExchange.Api.App.Features.Characteristics.Impl;

internal sealed partial class CharacteristicApiService(GroupedCharacteristicValidator validator) :
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