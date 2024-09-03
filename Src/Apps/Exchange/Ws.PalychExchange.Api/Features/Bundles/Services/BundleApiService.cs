using Ws.PalychExchange.Api.Common;
using Ws.PalychExchange.Api.Dto;
using Ws.PalychExchange.Api.Features.Bundles.Common;
using Ws.PalychExchange.Api.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.Features.Bundles.Services;

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