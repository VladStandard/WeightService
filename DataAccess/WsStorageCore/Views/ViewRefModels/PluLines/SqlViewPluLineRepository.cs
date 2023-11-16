namespace WsStorageCore.Views.ViewRefModels.PluLines;

// TODO: fix repository
public class SqlViewPluLineRepository : IViewPluLineRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;

    public IEnumerable<SqlViewPluLineModel> GetEnumerable(SqlCrudConfigModel sqlCrudConfig) =>
        GetEnumerable(0, new List<ushort>(), sqlCrudConfig.SelectTopRowsCount);

    public async Task <IEnumerable<SqlViewPluLineModel>> GetEnumerableAsync(SqlCrudConfigModel sqlCrudConfig) =>
        await GetEnumerableAsync(0, new List<ushort>(), sqlCrudConfig.SelectTopRowsCount);

    public IEnumerable<SqlViewPluLineModel> GetEnumerable(ushort scaleId = 0, int topRecords = 0) =>
        GetEnumerable(scaleId, new List<ushort>(), topRecords);
       
    public IEnumerable<SqlViewPluLineModel> GetEnumerable(ushort scaleId, ushort pluNumber, int topRecords) =>
        GetEnumerable(scaleId, new List<ushort> { pluNumber }, topRecords);

    public async Task<IEnumerable<SqlViewPluLineModel>> GetEnumerableAsync(ushort scaleId, IEnumerable<ushort> pluNumbers, int topRecords)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        return GetEnumerable(scaleId, pluNumbers, topRecords);
    }

    public IEnumerable<SqlViewPluLineModel> GetEnumerable(ushort scaleId, IEnumerable<ushort> pluNumbers, int topRecords)
    {
        List<SqlViewPluLineModel> result = new();
        string query = SqlQueriesDiags.Views.GetViewPlusScales(scaleId, pluNumbers, topRecords);
        object[] objects = SqlCore.GetArrayObjects(query);
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