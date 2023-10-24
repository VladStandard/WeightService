using WsStorageCore.WebApiModels.BarCodes;
namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class ItemBarCode : ItemBase<WsSqlBarCodeEntity>
{
    #region Public and private fields, properties, constructor

    public ItemBarCode() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }

    #endregion

    #region Public and private methods

    private string GetBarcodeTop()
    {
        WsSqlBarcodeTopModel barcodeTop = new(SqlItemCast.ValueTop, false);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeTopModel>(barcodeTop, WsEnumFormatType.Json, false);
    }

    private string GetBarcodeRight()
    {
        WsSqlBarcodeRightModel barcodeRight = new(SqlItemCast.ValueRight);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeRightModel>(barcodeRight, WsEnumFormatType.Json, false);
    }

    private string GetBarcodeBottom()
    {
        WsSqlBarcodeBottomModel barcodeBottom = new(SqlItemCast.ValueBottom);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeBottomModel>(barcodeBottom, WsEnumFormatType.Json, false);
    }

    #endregion
}
