using Ws.Labels.Dto;
using Ws.Labels.Services;
using Ws.Services.Dto;

namespace Ws.Services.Services.PrintLabel;

public class PrintLabelService : IPrintLabelService
{
    public string GenerateLabel(LabelInfoDto labelInfo)
    {
        LabelDto label = new LabelGenerator().GenerateLabel(labelInfo);
        return label.Context;
    }
}