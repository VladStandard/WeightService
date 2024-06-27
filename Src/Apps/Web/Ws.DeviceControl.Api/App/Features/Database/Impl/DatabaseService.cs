using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.DeviceControl.Api.App.Features.Database.Common;
using Ws.DeviceControl.Models.Models.Database;

namespace Ws.DeviceControl.Api.App.Features.Database.Impl;

public class DatabaseService(WsDbContext dbContext) : IDatabaseService
{
    public List<MigrationHistoryEntry> GetAllMigrations()
    {
        FormattableString getMigrationSql = FormattableStringFactory.Create("SELECT * FROM dbo.__EFMigrationsHistory");
        List<MigrationHistoryEntry> data = dbContext.Database.SqlQuery<MigrationHistoryEntry>(getMigrationSql).ToList();
        data.Reverse();
        return data;
    }
}