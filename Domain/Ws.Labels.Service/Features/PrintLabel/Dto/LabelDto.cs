using Ws.Labels.Service.Features.PrintLabel.Common;

namespace Ws.Labels.Service.Features.PrintLabel.Dto;

public class LabelDto(string zpl, ILabelModel labelModel)
{
    public string Context { get; set; } = zpl;
    public string BarcodeRight { get; set; } = labelModel.BarCodeRight;
    public string BarcodeBottom { get; set; } = labelModel.BarCodeBottom;
    public string BarcodeTop { get; set; } = labelModel.BarCodeTop;
}