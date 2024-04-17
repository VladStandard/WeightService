using Ws.Labels.Service.Features.PrintLabel.Models.XmlLabelBase;

namespace Ws.Labels.Service.Features.PrintLabel.Models;

internal record ZplInfo
{
    public readonly string Zpl;
    public readonly string BarcodeRight;
    public readonly string BarcodeBottom;
    public readonly string BarcodeTop;

    public ZplInfo(string zpl, XmlLabelBaseModel labelBarcodeModel)
    {
        Zpl = zpl;
        BarcodeRight = labelBarcodeModel.BarCodeRight;
        BarcodeBottom = labelBarcodeModel.BarCodeBottom;
        BarcodeTop = labelBarcodeModel.BarCodeTop;
    }
}