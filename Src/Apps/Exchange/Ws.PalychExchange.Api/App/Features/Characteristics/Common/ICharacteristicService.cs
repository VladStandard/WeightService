using Ws.PalychExchange.Api.App.Features.Characteristics.Impl.Models;

namespace Ws.PalychExchange.Api.App.Features.Characteristics.Common;

public interface ICharacteristicService
{
    public ResponseDto Load(HashSet<GroupedCharacteristic> dtos);
}