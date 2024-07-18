using Ws.DeviceControl.Models.Dto.Database;

namespace Ws.DeviceControl.Api.App.Features.Diag.Database.Common;

public interface IDatabaseService
{
    List<MigrationHistoryDto> GetAllMigrations();
    List<DataBaseTableDto> GetAllTables();
}