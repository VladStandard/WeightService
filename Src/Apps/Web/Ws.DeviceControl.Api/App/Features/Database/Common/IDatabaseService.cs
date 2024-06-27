using Ws.DeviceControl.Models.Models.Database;

namespace Ws.DeviceControl.Api.App.Features.Database.Common;

public interface IDatabaseService
{
    List<MigrationHistoryEntry> GetAllMigrations();
}