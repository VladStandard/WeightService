using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes.Common;

public interface IBoxService
{
    ResponseDto Load(BoxesWrapper dtoWrapper);
}