using Ws.DeviceControl.Models.Features.Database;

namespace Ws.DeviceControl.Api.App.Features.Diag.Database.Common;

public interface IDatabaseService
{
    List<MigrationHistoryDto> GetAllMigrations();
    List<DataBaseTableDto> GetAllTables();
}