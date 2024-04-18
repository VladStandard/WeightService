using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Characteristics.Dto;

namespace Ws.PalychExchangeApi.Features.Characteristics.Common;

public interface ICharacteristicService
{
    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper);
}