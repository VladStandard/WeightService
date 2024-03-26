using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles.Common;

public interface IBundleService
{
    BundleWrapper Load(BundleWrapper dtoWrapper);
}