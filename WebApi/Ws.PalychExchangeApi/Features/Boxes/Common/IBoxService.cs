using Ws.PalychExchangeApi.Features.Boxes.Dto;

namespace Ws.PalychExchangeApi.Features.Boxes.Common;

public interface IBoxService
{
    BoxWrapper Load(BoxWrapper dtoWrapper);
}