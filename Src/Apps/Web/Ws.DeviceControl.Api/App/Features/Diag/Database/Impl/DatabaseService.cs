using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Ws.Database.EntityFramework;
using Ws.DeviceControl.Api.App.Features.Diag.Database.Common;
using Ws.DeviceControl.Models.Dto.Database;

namespace Ws.DeviceControl.Api.App.Features.Diag.Database.Impl;

public class DatabaseService(WsDbContext dbContext) : IDatabaseService
{
    public List<MigrationHistoryDto> GetAllMigrations()
    {
        FormattableString getMigrationSql = FormattableStringFactory.Create("SELECT * FROM dbo.__EFMigrationsHistory");
        List<MigrationHistoryDto> data = dbContext.Database.SqlQuery<MigrationHistoryDto>(getMigrationSql).ToList();
        data.Reverse();
        return data;
    }

    public List<DataBaseTableDto> GetAllTables()
    {
        return dbContext.DatabaseTables.Select(i => new DataBaseTableDto
            {
                Schema = i.Schema,
                Table = i.Table,
                Rows = i.Rows,
                UsedMb = i.UsedMb ?? 0,
                FileName = i.FileName
            })
            .OrderBy(i => i.FileName)
            .ThenBy(i => i.Schema)
            .ThenByDescending(i => i.UsedMb)
            .ThenByDescending(i => i.Rows).ToList();
    }
}