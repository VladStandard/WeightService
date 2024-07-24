using Refit;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Create;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Commands.Update;
using Ws.DeviceControl.Models.Dto.Admins.PalletMen.Queries;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Create;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Arms.Queries;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Create;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Commands.Update;
using Ws.DeviceControl.Models.Dto.Devices.Printers.Queries;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.ProductionSites.Queries;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Create;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Warehouses.Queries;
using Ws.DeviceControl.Models.Dto.References1C.Brands;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Dto.References1C.Plus.Queries;
using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    # region Admin

    [Get("/pallet-men?productionSite={productionSiteUid}")]
    Task<PalletManDto[]> GetPalletMenByProductionSite(Guid productionSiteUid);

    [Get("/pallet-men/{uid}")]
    Task<PalletManDto> GetPalletManByUid(Guid uid);

    [Get("/pallet-men/{uid}")]
    Task<PalletManDto> GetPalletMan(Guid uid);

    [Post("/pallet-men")]
    Task<PalletManDto> CreatePalletMan([Body] PalletManCreateDto createDto);

    [Post("/pallet-men/{uid}")]
    Task<PalletManDto> UpdatePalletMan(Guid uid, [Body] PalletManUpdateDto updateDto);

    [Delete("/pallet-men/{uid}")]
    Task<bool> DeletePalletMan(Guid uid);

    # endregion

    # region Devices

    # region Arm

    [Get("/arms?productionSite={productionSiteUid}")]
    Task<ArmDto[]> GetArmsByProductionSite(Guid productionSiteUid);

    [Get("/arms/{uid}")]
    Task<ArmDto> GetArmByUid(Guid uid);

    [Post("/arms")]
    Task<ArmDto> CreateArm([Body] ArmCreateDto createDto);

    [Post("/arms/{uid}")]
    Task<ArmDto> UpdateArm(Guid uid, [Body] ArmUpdateDto updateDto);

    [Delete("/arms/{uid}")]
    Task<bool> DeleteArm(Guid uid);

    # endregion

    # region Printer

    [Get("/printers?productionSite={productionSiteUid}")]
    Task<PrinterDto[]> GetPrintersByProductionSite(Guid productionSiteUid);

    [Get("/printers/{uid}")]
    Task<PrinterDto> GetPrinterByUid(Guid uid);

    [Post("/printers")]
    Task<PrinterDto> CreatePrinter([Body] PrinterCreateDto createDto);

    [Post("/printers/{uid}")]
    Task<PrinterDto> UpdatePrinter(Guid uid, [Body] PrinterUpdateDto updateDto);

    [Delete("/printers/{uid}")]
    Task<bool> DeletePrinter(Guid uid);

    [Get("/printers/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyPrintersByProductionSite(Guid productionSiteUid);

    # endregion

    # endregion

    # region References 1C

    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();

    [Get("/boxes")]
    Task<PackageDto[]> GetBoxes();

    [Get("/boxes/{uid}")]
    Task<PackageDto> GetBoxByUid(Guid uid);

    [Get("/brands")]
    Task<BrandDto[]> GetBrands();

    [Get("/brands/{uid}")]
    Task<BrandDto> GetBrandByUid(Guid uid);

    [Get("/clips")]
    Task<PackageDto[]> GetClips();

    [Get("/clips/{uid}")]
    Task<PackageDto> GetClipByUid(Guid uid);

    [Get("/bundles")]
    Task<PackageDto[]> GetBundles();

    [Get("/bundles/{uid}")]
    Task<PackageDto> GetBundleByUid(Guid uid);

    [Get("/plu")]
    Task<PluDto[]> GetPlus();

    [Get("/plu/{uid}")]
    Task<PluDto> GetPluByUid(Guid uid);

    [Post("/plu/{uid}")]
    Task<PluDto> UpdatePlu(Guid uid, [Body] PluUpdateDto updateDto);

    # endregion

    # region References

    # region Production Site

    [Get("/production-sites")]
    Task<ProductionSiteDto[]> GetProductionSites();

    [Get("/production-sites/{uid}")]
    Task<ProductionSiteDto> GetProductionSiteByUid(Guid uid);

    [Post("/production-sites")]
    Task<ProductionSiteDto> CreateProductionSite([Body] ProductionSiteCreateDto createDto);

    [Post("/production-sites/{uid}")]
    Task<ProductionSiteDto> UpdateProductionSite(Guid uid, [Body] ProductionSiteUpdateDto updateDto);

    [Delete("/production-sites/{uid}")]
    Task<bool> DeleteProductionSite(Guid uid);

    # endregion

    # region Warehouse

    [Get("/warehouses?productionSite={productionSiteUid}")]
    Task<WarehouseDto[]> GetWarehousesByProductionSite(Guid productionSiteUid);

    [Get("/warehouses/{uid}")]
    Task<WarehouseDto> GetWarehouseByUid(Guid uid);

    [Post("/warehouses")]
    Task<WarehouseDto> CreateWarehouse([Body] WarehouseCreateDto createDto);

    [Post("/warehouses/{uid}")]
    Task<WarehouseDto> UpdateWarehouse(Guid uid, [Body] WarehouseUpdateDto updateDto);

    [Delete("/warehouses/{uid}")]
    Task<bool> DeleteWarehouse(Guid uid);

    [Get("/warehouses/proxy?productionSite={productionSiteUid}")]
    Task<ProxyDto[]> GetProxyWarehousesByProductionSite(Guid productionSiteUid);

    # endregion

    # endregion
}