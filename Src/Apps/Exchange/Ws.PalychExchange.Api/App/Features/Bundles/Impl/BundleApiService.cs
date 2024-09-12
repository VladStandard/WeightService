using Ws.PalychExchange.Api.App.Features.Bundles.Common;
using Ws.PalychExchange.Api.App.Features.Bundles.Dto;

namespace Ws.PalychExchange.Api.App.Features.Bundles.Impl;

internal sealed partial class BundleApiService(BundleDtoValidator validator) : BaseService<BundleDto>(validator), IBundleService
{
    public ResponseDto Load(HashSet<BundleDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        FilterValidDtos(dtos);
        SaveBundles(dtos);
        return OutputDto;
    }
}