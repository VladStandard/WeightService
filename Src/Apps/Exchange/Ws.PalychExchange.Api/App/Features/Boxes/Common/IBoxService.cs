using Ws.PalychExchange.Api.App.Features.Boxes.Dto;

namespace Ws.PalychExchange.Api.App.Features.Boxes.Common;

public interface IBoxService
{
    ResponseDto Load(BoxesWrapper dtoWrapper);
}