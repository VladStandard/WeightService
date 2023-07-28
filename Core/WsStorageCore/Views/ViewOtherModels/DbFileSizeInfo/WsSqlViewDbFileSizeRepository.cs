// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewOtherModels.DbFileSizeInfo;

public class WsSqlViewDbFileSizeRepository : IViewDbFileSizeRepository
{
    private static string GetDbFileSizes()
    {
        return WsSqlQueries.TrimQuery(@"
            SELECT
                [TYPE],
                [NAME] [FILE_NAME],
                [SIZE] * 8 / 1024 [SIZE_MB],
                [MAX_SIZE] * 8 / 1024 [MAX_SIZE_MB]
            FROM [SYS].[DATABASE_FILES]
            ORDER BY [SIZE_MB] DESC, [NAME];
        ");
    }

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    private static WsSqlViewDbFileSizeInfoModel? ParseViewModel(object obj)
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

    public List<WsSqlViewDbFileSizeInfoModel> GetList()
    {
        object[] objects = SqlCore.GetArrayObjectsNotNullable(GetDbFileSizes());
        List<WsSqlViewDbFileSizeInfoModel> result = new();
        foreach (object obj in objects)
        {
            WsSqlViewDbFileSizeInfoModel? model = ParseViewModel(obj);
            if (model != null)
                result.Add(model);
        }
        return result;
    }
}