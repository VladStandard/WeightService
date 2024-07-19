using Refit;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;
using Ws.DeviceControl.Models.Dto.References1C.Brands;
using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    # region Devices

    [Get("/printers?productionSite={productionSiteUid}")]
    Task<PrinterDto[]> GetPrinters(Guid productionSiteUid);

    [Get("/printers/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyPrinters(Guid productionSiteUid);

    [Get("/printers/{Uid}")]
    Task<PrinterDto> GetPrinterByUid(Guid uid);

    # endregion

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
    Task<ProductionSiteDto> GetProductionSiteByUid(Guid uid);

    [Get("/warehouses?productionSite={productionSiteUid}")]
    Task<WarehouseDto[]> GetWarehouses(Guid productionSiteUid);

    [Get("/warehouses/{Uid}")]
    Task<WarehouseDto> GetWarehouseByUid(Guid uid);

    [Get("/warehouses/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyWarehouses(Guid productionSiteUid);

    # endregion
}