using Phetch.Core;
using Ws.DeviceControl.Models;
using Ws.DeviceControl.Models.Features.Database;

namespace DeviceControl.Source.Shared.Services;

public sealed class DiagnosticEndpoints(IWebApi webApi)
{
    public ParameterlessEndpoint<MigrationHistoryDto[]> MigrationsEndpoint { get; } = new(
        webApi.GetMigrations,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });

    public ParameterlessEndpoint<DataBaseTableDto[]> TablesEndpoint { get; } = new(
        webApi.GetTables,
        options: new()
        {
            DefaultStaleTime = TimeSpan.FromMinutes(5),
        });
}