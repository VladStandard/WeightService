using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Plus.Dto;

namespace Ws.PalychExchange.Api.Features.Plus.Common;

public interface IPluService
{
    public ResponseDto Load(PlusWrapper dtoWrapper);
}