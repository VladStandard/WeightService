using Ws.PalychExchange.Api.App.Features.Plus.Dto;

namespace Ws.PalychExchange.Api.App.Features.Plus.Common;

public interface IPluService
{
    public ResponseDto Load(HashSet<PluDto> dtos);
}