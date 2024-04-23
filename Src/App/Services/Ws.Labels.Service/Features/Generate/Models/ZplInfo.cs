using XmlLabelBaseModel = Ws.Labels.Service.Features.Generate.Models.XmlLabelBase.XmlLabelBaseModel;

namespace Ws.Labels.Service.Features.Generate.Models;

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