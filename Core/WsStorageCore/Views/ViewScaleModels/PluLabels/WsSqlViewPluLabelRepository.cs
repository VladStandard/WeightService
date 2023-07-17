namespace WsStorageCore.Views.ViewScaleModels.PluLabels;

public class WsSqlViewPluLabelRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewPluLabelRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewPluLabelRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewPluLabelModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewPluLabelModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetPluLabels(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 10 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                CreateDt = Convert.ToDateTime(item[i++]),
                IsMarked = Convert.ToBoolean(item[i++]),
                ProductDate = Convert.ToDateTime(item[i++]),
                ExpirationDate = Convert.ToDateTime(item[i++]),
                WeightingDate = Convert.ToDateTime(item[i++]),
                Line = item[i++] as string ?? string.Empty,
                PluNumber = Convert.ToInt32(item[i++]),
                PluName = item[i++] as string ?? string.Empty,
                Template = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
}