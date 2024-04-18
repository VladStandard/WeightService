using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles.Common;

public interface IBundleService
{
    ResponseDto Load(BundlesWrapper dtoWrapper);
}