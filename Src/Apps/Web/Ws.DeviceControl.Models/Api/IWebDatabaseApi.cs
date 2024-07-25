using Ws.DeviceControl.Models.Dto.Database;

namespace Ws.DeviceControl.Models.Api;

public interface IWebDatabaseApi
{
    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();
}