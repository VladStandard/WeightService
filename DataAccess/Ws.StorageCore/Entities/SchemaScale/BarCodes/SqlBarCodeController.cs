namespace Ws.StorageCore.Entities.SchemaScale.BarCodes;


// TODO: refactor barcode controller
/// <summary>
/// Barcode helper.
/// </summary>
public class SqlBarCodeController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static SqlBarCodeController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static SqlBarCodeController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    public void SetBarCodeTop(SqlBarCodeEntity barCode, SqlPluLabelContextModel pluLabelContext)
    {
        barCode.ValueTop = pluLabelContext.BarCodeTop;
    }
    
    public void SetBarCodeRight(SqlBarCodeEntity barCode, SqlPluLabelContextModel pluLabelContext)
    {
        barCode.ValueRight = pluLabelContext.BarCodeRight;
    }
    
    public void SetBarCodeBottom(SqlBarCodeEntity barCode, SqlPluLabelContextModel pluLabelContext)
    {
        barCode.ValueBottom = pluLabelContext.BarCodeBottom;
    }
}