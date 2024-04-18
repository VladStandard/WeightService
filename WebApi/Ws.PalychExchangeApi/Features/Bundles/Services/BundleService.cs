using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

internal partial class BundleService(BundleDtoValidator validator) : BaseService<BundleDto>(validator), IBundleService
{
    public ResponseDto Load(BundlesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Bundles);
        IEnumerable<BundleDto> validDtos = FilterValidDtos(dtoWrapper.Bundles);
        SaveBundles(validDtos);
        return OutputDto;
    }
}