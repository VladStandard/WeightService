using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Plus.Dto;

namespace Ws.PalychExchangeApi.Features.Plus.Common;

public interface IPluService
{
    public ResponseDto Load(PlusWrapper dtoWrapper);
}