using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;
using Ws.StorageCore.Entities;
using Ws.StorageCore.Entities.Diag.TableSizes;

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