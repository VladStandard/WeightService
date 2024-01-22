using Ws.Domain.Models.Entities;

namespace Ws.Database.Core.Entities;

public class SqlViewDbFileSizeRepository
{
    private static string GetDbFileSizes()
    {
        return SqlQueries.TrimQuery(@"
            SELECT
                [TYPE],
                [NAME] [FILE_NAME],
                [SIZE] * 8 / 1024 [SIZE_MB],
                [MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
            FROM [SYS].[DATABASE_FILES]
            ORDER BY [SIZE_MB] DESC, [NAME];
        ");
    }

    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    private static DbFileSizeInfoEntity? ParseViewModel(object obj)
    {
        int i = 0;
        if (obj is object[] { Length: 4 } item)
            return new(
                Convert.ToByte(item[i++]),
                Convert.ToString(item[i++]),
                Convert.ToUInt16(item[i++]),
                Convert.ToUInt16(item[i])
            );
        return null;
    }

    public List<DbFileSizeInfoEntity> GetList()
    {
        IEnumerable<Object> objects = SqlCore.GetArrayObjects(GetDbFileSizes());
        List<DbFileSizeInfoEntity> result = [];
        result.AddRange(objects.Select(ParseViewModel).OfType<DbFileSizeInfoEntity>());
        return result;
    }
}