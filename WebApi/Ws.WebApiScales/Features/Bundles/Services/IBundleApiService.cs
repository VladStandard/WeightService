using Ws.WebApiScales.Features.Bundles.Dto;

namespace Ws.WebApiScales.Features.Bundles.Services;

internal interface IBundleApiService
{
    public void Load(BundlesWrapper bundleWrapper);
}