namespace WsStorageCore.Views.ViewScaleModels.PluWeightings;

public class SqlViewPluWeightingRepository : IViewPluWeightingRepository
{
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
    
    public IList<SqlViewPluWeightingModel> GetList(SqlCrudConfigModel sqlCrudConfig)
    {
        IList<SqlViewPluWeightingModel> result = new List<SqlViewPluWeightingModel>();
        string query = SqlQueriesDiags.Views.GetPluWeightings(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjects(query);
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