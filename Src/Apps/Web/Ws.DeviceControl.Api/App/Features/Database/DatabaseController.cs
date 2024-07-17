using Microsoft.AspNetCore.Mvc;
using Ws.DeviceControl.Api.App.Features.Database.Common;
using Ws.DeviceControl.Models.Models.Database;

namespace Ws.DeviceControl.Api.App.Features.Database;

[ApiController]
[Route("api/database/")]
public class DatabaseController(IDatabaseService databaseService)
{
    #region Queries

    [HttpGet("migrations")]
    public List<MigrationHistoryEntry> GetAllMigrations() => databaseService.GetAllMigrations();

    [HttpGet("tables")]
    public List<DataBaseTableEntry> GetAllTables() => databaseService.GetAllTables();

    #endregion
}