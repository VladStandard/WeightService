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

    public List<DataBaseTableEntry> GetAllTables()
    {
        var data = dbContext.Database.SqlQuery<DataBaseTableEntry>(
            $"""
             SELECT
                         [SCHEMA] AS [{nameof(DataBaseTableEntry.Schema)}],
                         [TABLE] AS [{nameof(DataBaseTableEntry.Table)}],
                         [ROWS_COUNT] AS [{nameof(DataBaseTableEntry.Rows)}],
                         [USED_MB] AS [{nameof(DataBaseTableEntry.UsedMb)}],
                         [FILENAME] AS [{nameof(DataBaseTableEntry.FileName)}]
                       FROM dbo.DATABASE_TABLES_VIEW
             """
        ).ToList();
        return new();
    }
}