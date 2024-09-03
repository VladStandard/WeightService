using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.Features.Boxes.Common;

public interface IBoxService
{
    ResponseDto Load(BoxesWrapper dtoWrapper);
}