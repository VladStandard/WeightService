namespace WsStorageCore.Views.ViewScaleModels.Barcodes;

public class WsSqlViewBarcodeRepository : IViewBarcodeRepository
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlViewBarcodeRepository _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlViewBarcodeRepository Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    
    public List<WsSqlViewBarcodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        List<WsSqlViewBarcodeModel> result = new();
        string query = WsSqlQueriesDiags.Views.GetBarcodes(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        foreach (var obj in objects)
        {
            int i = 0;
            if (obj is not object[] item || item.Length < 7 || !Guid.TryParse(Convert.ToString(item[i++]), out var uid)) break;
            result.Add(new()
            {
                IdentityValueUid = uid,
                IsMarked = Convert.ToBoolean(item[i++]),
                CreateDt = Convert.ToDateTime(item[i++]),
                PluNumber = Convert.ToInt32(item[i++]),
                ValueTop = item[i++] as string ?? string.Empty,
                ValueRight = item[i++] as string ?? string.Empty,
                ValueBottom = item[i++] as string ?? string.Empty
            });
        }
        return result;
    }
}