using Ws.Database.Core.Entities;
using Ws.Database.Core.Entities.Diag.TableSizes;
using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Services.Features.DatabaseFile;

internal class DatabaseFileService : IDatabaseFileService
{
    public IEnumerable<DbFileSizeInfoEntity> GetAll()
    {
        List<DbFileSizeInfoEntity> sqlFiles = new SqlViewDbFileSizeRepository().GetList();
        List<TableSizeEntity> sqlTables = new SqlViewTableSizeRepository().GetEnumerable().ToList();
        foreach (DbFileSizeInfoEntity sqlFile in sqlFiles)
            sqlFile.Tables.AddRange(sqlTables.Where(table => table.FileName == sqlFile.FileName));
        return sqlFiles;
    }
}