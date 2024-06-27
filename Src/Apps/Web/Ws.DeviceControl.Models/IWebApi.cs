using Refit;
using Ws.DeviceControl.Models.Models.Database;

namespace Ws.DeviceControl.Models;

public interface IWebApi
{
    [Get("/api/database/migrations")]
    Task<MigrationHistoryEntry[]> GetMigrations();

    [Get("/api/database/tables")]
    Task<DataBaseTableEntry[]> GetTables();
}
