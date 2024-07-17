using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Models.Database;

namespace DeviceControl.Source.Shared.Services;

public class DatabaseApi(IWebApi webApi)
{
    public ParameterlessEndpoint<MigrationHistoryEntry[]> MigrationsEndpoint { get; } = new(
        webApi.GetMigrations,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });

    public ParameterlessEndpoint<DataBaseTableEntry[]> TablesEndpoint { get; } = new(
        webApi.GetTables,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });
}