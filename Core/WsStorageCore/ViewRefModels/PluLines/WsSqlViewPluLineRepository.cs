namespace WsStorageCore.ViewRefModels.PluLines;

public class WsSqlViewPluLineRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewPluLineRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewPluLineRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluLineModel> GetList(ushort scaleId = 0, int topRecords = 0) =>
        GetList(scaleId, new List<ushort>(), topRecords);
       
    public List<WsSqlViewPluLineModel> GetList(ushort scaleId, ushort pluNumber, int topRecords) =>
        GetList(scaleId, new List<ushort> { pluNumber }, topRecords);
    
    public List<WsSqlViewPluLineModel> GetList(ushort scaleId, List<ushort> pluNumbers, int topRecords)
    {
        List<WsSqlViewPluLineModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetViewPlusScales(scaleId, pluNumbers, topRecords);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 19) break;
            result.Add(new(Guid.Parse(Convert.ToString(item[i++])),
                Convert.ToDateTime(item[i++]), Convert.ToDateTime(item[i++]),
                Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToString(item[i++]),
                Guid.Parse(Convert.ToString(item[i++])), Convert.ToBoolean(item[i++]), Convert.ToBoolean(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToString(item[i++]),
                Convert.ToString(item[i++]), Convert.ToString(item[i++]), Convert.ToString(item[i++]),
                Convert.ToUInt16(item[i++]), Convert.ToBoolean(item[i++]), Convert.ToString(item[i++])
            ));
        }

        return result;
    }
}