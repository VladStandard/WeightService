using Refit;
using Ws.DeviceControl.Models.Models.Database;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    [Get("/database/migrations")]
    Task<MigrationHistoryEntry[]> GetMigrations();

    [Get("/database/tables")]
    Task<DataBaseTableEntry[]> GetTables();
}