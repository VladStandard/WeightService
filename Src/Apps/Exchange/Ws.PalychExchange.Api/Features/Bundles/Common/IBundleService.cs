using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.Features.Bundles.Common;

public interface IBundleService
{
    ResponseDto Load(BundlesWrapper dtoWrapper);
}