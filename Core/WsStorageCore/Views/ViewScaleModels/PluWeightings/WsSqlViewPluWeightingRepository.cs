// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Views.ViewScaleModels.PluWeightings;

public class WsSqlViewPluWeightingRepository : IViewPluWeightingRepository
{
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public IList<WsSqlViewPluWeightingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        IList<WsSqlViewPluWeightingModel> result = new List<WsSqlViewPluWeightingModel>();
        string query = WsSqlQueriesDiags.Views.GetPluWeightings(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);
        foreach (object obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 8 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                CreateDt = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                PluName = item[i++] as string ?? string.Empty,
                TareWeight = Convert.ToDecimal(item[i++]),
                NettoWeight = Convert.ToDecimal(item[i++])
            });
        }
        return result;
    }
}