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
        return WsDataFormatUtils.SerializeAsJson(barcodeTop);
    }

    private string GetBarcodeRight()
    {
        WsSqlBarcodeRightModel barcodeRight = new(SqlItemCast.ValueRight);
        return WsDataFormatUtils.SerializeAsJson(barcodeRight);
    }

    private string GetBarcodeBottom()
    {
        WsSqlBarcodeBottomModel barcodeBottom = new(SqlItemCast.ValueBottom);
        return WsDataFormatUtils.SerializeAsJson(barcodeBottom);
    }

    #endregion
}
