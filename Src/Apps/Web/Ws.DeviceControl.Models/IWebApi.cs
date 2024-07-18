using Refit;
using Ws.DeviceControl.Models.Dto.Database;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    [Get("/database/migrations")]
    Task<MigrationHistoryDto[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableDto[]> GetTables();
}