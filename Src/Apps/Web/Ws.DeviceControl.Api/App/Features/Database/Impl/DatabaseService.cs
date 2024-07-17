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
        return dbContext.DatabaseTables.Select(i => new DataBaseTableEntry
            {
                Schema = i.Schema,
                Table = i.Table,
                Rows = i.Rows,
                UsedMb = i.UsedMb,
                FileName = i.FileName
            })
            .OrderBy(i => i.FileName)
            .ThenBy(i => i.Schema)
            .ThenByDescending(i => i.UsedMb)
            .ThenByDescending(i => i.Rows).ToList();
    }
}