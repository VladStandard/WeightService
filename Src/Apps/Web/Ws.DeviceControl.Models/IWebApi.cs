using Refit;
using Ws.DeviceControl.Models.Dto.Database;
using Ws.DeviceControl.Models.Dto.Shared;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();

    [Get("/boxes")]
    Task<PackageDto[]> GetBoxes();

    [Get("/boxes/{Uid}")]
    Task<PackageDto> GetBoxByUid(Guid uid);
}