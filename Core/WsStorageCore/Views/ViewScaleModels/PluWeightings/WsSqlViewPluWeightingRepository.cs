namespace WsStorageCore.Views.ViewScaleModels.PluWeightings;

public class WsSqlViewPluWeightingRepository : IViewPluWeightingRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewPluWeightingRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewPluWeightingRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluWeightingModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluWeightingModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluWeightings(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
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