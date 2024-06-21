using Ws.Labels.Service.Generate.Models.XmlLabelBase;

namespace Ws.Labels.Service.Generate.Models;

internal record ZplInfo
{
    public readonly string Zpl;
    public readonly string BarcodeRight;
    public readonly string BarcodeBottom;
    public readonly string BarcodeTop;

    public ZplInfo(string zpl, BarcodeLabelLabel labelLabelBarcode)
    {
        Zpl = zpl;
        BarcodeRight = labelLabelBarcode.BarCodeRight;
        BarcodeBottom = labelLabelBarcode.BarCodeBottom;
        BarcodeTop = labelLabelBarcode.BarCodeTop;
    }
}