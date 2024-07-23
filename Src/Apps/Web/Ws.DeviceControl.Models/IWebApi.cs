using Refit;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;
using Ws.DeviceControl.Models.Dto.References1C.Brands;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;
using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    # region Admin

    [Get("/pallet-men?productionSite={productionSiteUid}")]
    Task<PalletManDto[]> GetPalletMenByProductionSite(Guid productionSiteUid);

    [Get("/pallet-men/{Uid}")]
    Task<PalletManDto> GetPalletManByUid(Guid uid);

    # endregion

    # region Devices

    [Get("/arms?productionSite={productionSiteUid}")]
    Task<ArmDto[]> GetArmsByProductionSite(Guid productionSiteUid);

    [Get("/arms/{Uid}")]
    Task<ArmDto> GetArmByUid(Guid uid);


    [Get("/printers?productionSite={productionSiteUid}")]
    Task<PrinterDto[]> GetPrintersByProductionSite(Guid productionSiteUid);

    [Get("/printers/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyPrintersByProductionSite(Guid productionSiteUid);

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

    [Get("/plu")]
    Task<PluDto[]> GetPlus();

    [Get("/plu/{Uid}")]
    Task<PluDto> GetPluByUid(Guid uid);

    # endregion

    # region References

    # region Production Site

    [Get("/production-sites")]
    Task<ProductionSiteDto[]> GetProductionSites();

    [Get("/production-sites/{Uid}")]
    Task<ProductionSiteDto> GetProductionSiteByUid(Guid uid);

    [Post("/production-sites")]
    Task<ProductionSiteDto> CreateProductionSite([Body] ProductionSiteCreateDto createDto);

    [Put("/production-sites/{productionSiteUid}")]
    Task<ProductionSiteDto> UpdateProductionSite(Guid productionSiteUid, [Body] ProductionSiteUpdateDto updateDto);

    [Delete("/production-sites/{productionSiteUid}")]
    Task<bool> DeleteProductionSite(Guid productionSiteUid);

    [Get("/warehouses?productionSite={productionSiteUid}")]
    Task<WarehouseDto[]> GetWarehousesByProductionSite(Guid productionSiteUid);

    # endregion

    [Get("/warehouses/{Uid}")]
    Task<WarehouseDto> GetWarehouseByUid(Guid uid);

    [Get("/warehouses/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyWarehousesByProductionSite(Guid productionSiteUid);

    # endregion
}