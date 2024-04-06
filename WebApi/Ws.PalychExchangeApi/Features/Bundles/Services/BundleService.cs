using Ws.Database.EntityFramework;
using Ws.PalychExchangeApi.Common;
using Ws.PalychExchangeApi.Dto;
using Ws.PalychExchangeApi.Features.Bundles.Common;
using Ws.PalychExchangeApi.Features.Bundles.Dto;

namespace Ws.PalychExchangeApi.Features.Bundles.Services;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
internal partial class BundleService(WsDbContext dbContext, BundleDtoValidator validator) :
    BaseService<BundleDto>(dbContext, validator), IBundleService
{
    public ResponseDto Load(BundlesWrapper dtoWrapper)
    {
        ResolveUniqueUidLocal(dtoWrapper.Bundles);
        IEnumerable<BundleDto> validDtos = FilterValidDtos(dtoWrapper.Bundles);
        SaveBundles(validDtos);
        return OutputDto;
    }
}