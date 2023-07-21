// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewRefModels.PluLines;

//TODO: fix repository
public class WsSqlViewPluLineRepository : IViewPluLineRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;

    public List<WsSqlViewPluLineModel> GetList(WsSqlCrudConfigModel sqlCrudConfig) =>
        GetList(0, new List<ushort>(), sqlCrudConfig.SelectTopRowsCount);

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