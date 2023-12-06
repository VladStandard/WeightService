using Ws.Labels.Common;

namespace Ws.Labels.Dto;

public class LabelDto
{
    public string Context { get; set; }
    public string BarcodeRight { get; set; }
    public string BarcodeBottom { get; set; }
    public string BarcodeTop { get; set; }

    public LabelDto(string zpl, ILabelModel labelModel)
    {
        Context = zpl;
        BarcodeTop = labelModel.BarCodeTop;
        BarcodeRight = labelModel.BarCodeRight;
        BarcodeBottom = labelModel.BarCodeBottom;
    }
}