namespace WsStorageCore.Entities.SchemaScale.BarCodes;


// TODO: refactor barcode controller
/// <summary>
/// Barcode helper.
/// </summary>
public class WsSqlBarCodeController
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsSqlBarCodeController _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsSqlBarCodeController Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    private string TypeBarCodeGs128 => "GS1-128";
    private string TypeBarCodeTop => "Interleaved 2 of 5 Bar Code";

    #endregion

    #region Public and private methods
    
    public void SetBarCodeTop(WsSqlBarCodeEntity barCode, WsSqlPluLabelContextModel pluLabelContext)
    {
        barCode.TypeTop = TypeBarCodeTop;
        barCode.ValueTop = pluLabelContext.BarCodeTop;
    }
    
    public void SetBarCodeRight(WsSqlBarCodeEntity barCode, WsSqlPluLabelContextModel pluLabelContext)
    {
        barCode.TypeRight = TypeBarCodeGs128;
        barCode.ValueRight = pluLabelContext.BarCodeRight;
    }
    
    public void SetBarCodeBottom(WsSqlBarCodeEntity barCode, WsSqlPluLabelContextModel pluLabelContext)
    {
        barCode.TypeBottom = TypeBarCodeGs128;
        barCode.ValueBottom = pluLabelContext.BarCodeBottom;
    }
    
    #endregion
}