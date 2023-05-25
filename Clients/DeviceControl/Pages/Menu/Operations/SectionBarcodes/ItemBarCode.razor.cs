// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.WebApiModels.BarCodes;

namespace DeviceControl.Pages.Menu.Operations.SectionBarcodes;

public sealed partial class ItemBarCode : RazorComponentItemBase<WsSqlBarCodeModel>
{
    #region Public and private fields, properties, constructor

    public ItemBarCode() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    private string GetBarcodeTop(WsEnumFormatType formatType)
    {
        WsSqlBarcodeTopModel barcodeTop = new(SqlItemCast.ValueTop, false);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeTopModel>(barcodeTop, formatType, true);
    }

    private string GetBarcodeRight(WsEnumFormatType formatType)
    {
        WsSqlBarcodeRightModel barcodeRight = new(SqlItemCast.ValueRight);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeRightModel>(barcodeRight, formatType, true);
    }

    private string GetBarcodeBottom(WsEnumFormatType formatType)
    {
        WsSqlBarcodeBottomModel barcodeBottom = new(SqlItemCast.ValueBottom);
        return WsDataFormatUtils.GetContent<WsSqlBarcodeBottomModel>(barcodeBottom, formatType, true);
    }

    #endregion
}
