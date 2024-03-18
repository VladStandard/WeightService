using Ws.Database.Nhibernate.Entities;
using Ws.Database.Nhibernate.Entities.Diag.TableSizes;
using Ws.Domain.Models.Entities;
using Ws.Domain.Models.Entities.Diag;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.DatabaseFile;

internal class DatabaseFileService(SqlViewDbFileSizeRepository viewDbFileSizeRepo) : IDatabaseFileService
{
    [Transactional]
    public IEnumerable<DbFileSizeInfoEntity> GetAll()
    {
        List<DbFileSizeInfoEntity> sqlFiles = viewDbFileSizeRepo.GetAll().ToList();
        List<TableSizeEntity> sqlTables = new SqlViewTableSizeRepository().GetAll().ToList();
        foreach (DbFileSizeInfoEntity sqlFile in sqlFiles)
            sqlFile.Tables.AddRange(sqlTables.Where(table => table.FileName == sqlFile.FileName));
        return sqlFiles;
    }
}