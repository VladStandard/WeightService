using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Dto.References1C.Brands;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;
using Ws.DeviceControl.Models.Dto.Shared;
using Ws.Shared.Extensions;

namespace DeviceControl.Source.Shared.Services;

public class References1CEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<PluDto[]> PlusEndpoint { get; } = new(
        webApi.GetPlus,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> BoxesEndpoint { get; } = new(
        webApi.GetBoxes,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> BundlesEndpoint { get; } = new(
        webApi.GetBundles,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<PackageDto[]> ClipsEndpoint { get; } = new(
        webApi.GetClips,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public ParameterlessEndpoint<BrandDto[]> BrandsEndpoint { get; } = new(
        webApi.GetBrands,
        options: new() { DefaultStaleTime = TimeSpan.FromMinutes(1) });

    public void UpdatePlu(PluDto plu) => PlusEndpoint.UpdateQueryData(new(),
        query => query.Data == null ? query.Data! : query.Data.ReplaceItemByKey(plu, p => p.Id == plu.Id).ToArray());
}