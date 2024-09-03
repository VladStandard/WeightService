using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Characteristics.Dto;

namespace Ws.PalychExchange.Api.Features.Characteristics.Common;

public interface ICharacteristicService
{
    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper);
}