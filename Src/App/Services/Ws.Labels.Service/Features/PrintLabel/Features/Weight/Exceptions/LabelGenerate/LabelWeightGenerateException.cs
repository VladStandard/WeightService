namespace Ws.Labels.Service.Features.PrintLabel.Features.Weight.Exceptions.LabelGenerate;

public class LabelWeightGenerateException(LabelGenExceptionEnum exception) : Exception
{
    public readonly LabelGenExceptionEnum Code = exception;
}