using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Common;

public interface IBundleService
{
    ResponseDto Load(BundlesWrapper dtoWrapper);
}