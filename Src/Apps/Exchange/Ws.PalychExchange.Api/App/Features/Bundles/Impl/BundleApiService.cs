using Ws.PalychExchange.Api.App.Features.Bundles.Common;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Impl;

internal sealed partial class BundleApiService(BundleDtoValidator validator, ILogger<BundleApiService> logger) : BaseService<BundleDto>(validator), IBundleService
{
    public ResponseDto Load(HashSet<BundleDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        FilterValidDtos(dtos);
        SaveBundles(dtos);
        return OutputDto;
    }
}