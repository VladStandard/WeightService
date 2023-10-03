namespace WsStorageCore.Views.ViewScaleModels.Barcodes;

public sealed class WsSqlViewBarcodeRepository : IViewBarcodeRepository
{
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
    
    public IList<WsSqlViewBarcodeModel> GetList(WsSqlCrudConfigModel sqlCrudConfig)
    {
        string query = WsSqlQueriesDiags.Views.GetBarcodes(sqlCrudConfig.SelectTopRowsCount, sqlCrudConfig.IsMarked);
        object[] objects = SqlCore.GetArrayObjects(query);
        return objects
            .Select(ParseViewModel)
            .Where(barcodeModel => barcodeModel.IsExists)
            .ToList();
    }
}