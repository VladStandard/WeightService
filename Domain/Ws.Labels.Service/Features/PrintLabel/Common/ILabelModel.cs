namespace Ws.Labels.Service.Features.PrintLabel.Common;

public interface ILabelModel
{
    string BarCodeTop { get; }
    string BarCodeRight { get; }
    string BarCodeBottom { get; }
}