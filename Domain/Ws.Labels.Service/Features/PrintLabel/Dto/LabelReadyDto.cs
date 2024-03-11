using Ws.Labels.Service.Features.PrintLabel.Common;

namespace Ws.Labels.Service.Features.PrintLabel.Dto;

internal class LabelReadyDto(string zpl, XmlLabelBaseModel labelBarcodeModel)
{
    public readonly string Zpl = zpl;
    public readonly string BarcodeRight = labelBarcodeModel.BarCodeRight;
    public readonly string BarcodeBottom = labelBarcodeModel.BarCodeBottom;
    public readonly string BarcodeTop = labelBarcodeModel.BarCodeTop;
}