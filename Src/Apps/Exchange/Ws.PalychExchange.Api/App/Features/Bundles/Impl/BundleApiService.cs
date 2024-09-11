using Ws.PalychExchange.Api.App.Features.Bundles.Common;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Impl;

internal sealed partial class BundleApiService(BundleDtoValidator validator) : BaseService<BundleDto>(validator), IBundleService
{
    public ResponseDto Load(BundlesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Bundles);
        IEnumerable<BundleDto> validDtos = FilterValidDtos(dtoWrapper.Bundles);
        SaveBundles(validDtos);
        return OutputDto;
    }
}