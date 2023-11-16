using Ws.StorageCore.WebApiModels.BarCodes;

namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class ItemBarCode : ItemBase<SqlBarCodeEntity>
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
        SqlBarcodeTopModel barcodeTop = new(SqlItemCast.ValueTop, false);
        return DataFormatUtils.SerializeAsJson(barcodeTop);
    }

    private string GetBarcodeRight()
    {
        SqlBarcodeRightModel barcodeRight = new(SqlItemCast.ValueRight);
        return DataFormatUtils.SerializeAsJson(barcodeRight);
    }

    private string GetBarcodeBottom()
    {
        SqlBarcodeBottomModel barcodeBottom = new(SqlItemCast.ValueBottom);
        return DataFormatUtils.SerializeAsJson(barcodeBottom);
    }

    #endregion
}
