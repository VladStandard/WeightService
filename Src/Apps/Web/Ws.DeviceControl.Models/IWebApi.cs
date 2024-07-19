using Refit;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References1C.Brands;
using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    # region References 1C

    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();

    [Get("/boxes")]
    Task<PackageDto[]> GetBoxes();

    [Get("/boxes/{Uid}")]
    Task<PackageDto> GetBoxByUid(Guid uid);

    [Get("/brands")]
    Task<BrandDto[]> GetBrands();

    [Get("/brands/{Uid}")]
    Task<BrandDto> GetBrandByUid(Guid uid);

    [Get("/clips")]
    Task<PackageDto[]> GetClips();

    [Get("/clips/{Uid}")]
    Task<PackageDto> GetClipByUid(Guid uid);

    [Get("/bundles")]
    Task<PackageDto[]> GetBundles();

    [Get("/bundles/{Uid}")]
    Task<PackageDto> GetBundleByUid(Guid uid);

    # endregion

    # region References

    [Get("/production-sites")]
    Task<ProductionSiteDto[]> GetProductionSites();

    [Get("/production-sites/{Uid}")]
    Task<ProductionSiteDto> GetProductionSitesByUid(Guid uid);

    # endregion
}