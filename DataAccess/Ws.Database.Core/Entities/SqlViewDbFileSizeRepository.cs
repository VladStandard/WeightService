using Ws.Domain.Models.Entities;

namespace Ws.Database.Core.Entities;

public class SqlViewDbFileSizeRepository
{
    public List<DbFileSizeInfoEntity> GetList()
    {
        const string fileNameAlias = nameof(DbFileSizeInfoEntity.FileName);
        const string sizeMbAlias = nameof(DbFileSizeInfoEntity.SizeMb);
        const string maxSizeMbAlias = nameof(DbFileSizeInfoEntity.MaxSizeMb);

        const string sqlQuery = 
            $"SELECT" +
            $"\n [NAME] AS [{fileNameAlias}]," +
            $"\n [SIZE] * 8 / 1024 AS [{sizeMbAlias}]," +
            $"\n [MAX_SIZE] * 8 / 1024 AS [{maxSizeMbAlias}]" +
            $"\n FROM [SYS].[DATABASE_FILES]" +
            $"\n ORDER BY [{sizeMbAlias}] DESC, [NAME]";

        return SqlCoreHelper.Instance.GetEnumerableBySql<DbFileSizeInfoEntity>(sqlQuery).ToList();
    }
}