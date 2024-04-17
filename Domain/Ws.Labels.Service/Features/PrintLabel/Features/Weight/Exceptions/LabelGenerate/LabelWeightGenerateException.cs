namespace Ws.Labels.Service.Features.PrintLabel.Features.Weight.Exceptions.LabelGenerate;

public class LabelWeightGenerateException(LabelGenExceptionEnum exception) : Exception
{
    public LabelGenExceptionEnum Code = exception;
}