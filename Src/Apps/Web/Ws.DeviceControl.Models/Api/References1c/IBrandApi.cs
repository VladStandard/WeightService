using Ws.DeviceControl.Models.Dto.References1C.Brands;

namespace Ws.DeviceControl.Models.Api.References1c;

public interface IBrandApi
{
    #region Queries

    [Get("/brands")]
    Task<BrandDto[]> GetBrands();

    [Get("/brands/{uid}")]
    Task<BrandDto> GetBrandByUid(Guid uid);

    #endregion
}