using Ws.Domain.Models.Entities;
// ReSharper disable UseRawString

namespace Ws.Database.Core.Entities;

public class SqlViewDbFileSizeRepository
{
    public List<DbFileSizeInfoEntity> GetList()
    {
        const string fileNameAlias = nameof(DbFileSizeInfoEntity.FileName);
        const string sizeMbAlias = nameof(DbFileSizeInfoEntity.SizeMb);
        const string maxSizeMbAlias = nameof(DbFileSizeInfoEntity.MaxSizeMb);

        const string sqlQuery = $@"
        SELECT
            [NAME] AS [{fileNameAlias}],
            [SIZE] * 8 / 1024 AS [{sizeMbAlias}],
            [MAX_SIZE] * 8 / 1024 AS [{maxSizeMbAlias}]
        FROM [SYS].[DATABASE_FILES]
        ORDER BY [{sizeMbAlias}] DESC, [NAME]";

        return SqlCoreHelper.Instance.GetArrayObjects<DbFileSizeInfoEntity>(sqlQuery).ToList();
    }
}