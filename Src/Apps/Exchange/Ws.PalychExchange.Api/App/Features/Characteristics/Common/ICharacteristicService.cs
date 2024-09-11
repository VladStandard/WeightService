using Ws.PalychExchange.Api.App.Features.Characteristics.Dto;

namespace Ws.PalychExchange.Api.App.Features.Characteristics.Common;

public interface ICharacteristicService
{
    public ResponseDto Load(PluCharacteristicsWrapper dtoWrapper);
}