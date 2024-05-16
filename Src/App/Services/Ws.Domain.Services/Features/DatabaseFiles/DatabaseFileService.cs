using Ws.Database.Nhibernate.Entities;
using Ws.Database.Nhibernate.Entities.Diag.TableSizes;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.DatabaseFiles;

internal class DatabaseFileService(SqlViewDbFileSizeRepository viewDbFileSizeRepo) : IDatabaseFileService
{
    [Transactional]
    public IEnumerable<DbFileSizeInfo> GetAll()
    {
        List<DbFileSizeInfo> sqlFiles = viewDbFileSizeRepo.GetAll().ToList();
        List<TableSize> sqlTables = new SqlViewTableSizeRepository().GetAll().ToList();
        foreach (DbFileSizeInfo sqlFile in sqlFiles)
            sqlFile.Tables.AddRange(sqlTables.Where(table => table.FileName == sqlFile.FileName));
        return sqlFiles;
    }
}