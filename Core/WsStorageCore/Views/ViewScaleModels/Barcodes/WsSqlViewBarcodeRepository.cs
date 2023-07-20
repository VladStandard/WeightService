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
    
    private static WsSqlViewBarcodeModel ParseViewModel(object obj) 
    {
        if (obj is not object[] item || item.Length < 7 || !Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
            return new ();

        int i = 1;
        return new ()
        {
            IdentityValueUid = uid,
            IsMarked = Convert.ToBoolean(item[i++]),
            CreateDt = Convert.ToDateTime(item[i++]),
            PluNumber = Convert.ToInt32(item[i++]),
            ValueTop = item[i++] as string ?? string.Empty,
            ValueRight = item[i++] as string ?? string.Empty,
            ValueBottom = item[i] as string ?? string.Empty
        };
    }
    
    public List<WsSqlViewBarcodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        string query = WsSqlQueriesDiags.Views.GetBarcodes(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjectsNotNullable(query);

        List<WsSqlViewBarcodeModel> result = objects
            .Select(ParseViewModel)
            .Where(barcodeModel => barcodeModel.IsNotNew)
            .ToList();

        return result;
    }
}