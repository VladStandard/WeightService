using Ws.DeviceControl.Api.App.Features.Diag.Database.Common;
using Ws.DeviceControl.Models.Dto.Database;

namespace Ws.DeviceControl.Api.App.Features.Diag.Database;

[ApiController]
[Route("api/database/")]
public class DatabaseController(IDatabaseService databaseService)
{
    #region Queries

    [HttpGet("migrations")]
    public List<MigrationHistoryDto> GetAllMigrations() => databaseService.GetAllMigrations();

    [HttpGet("tables")]
    public List<DataBaseTableDto> GetAllTables() => databaseService.GetAllTables();

    #endregion
}