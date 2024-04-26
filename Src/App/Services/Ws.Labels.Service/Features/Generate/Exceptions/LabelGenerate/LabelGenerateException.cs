namespace Ws.Labels.Service.Features.Generate.Exceptions.LabelGenerate;

public class LabelGenerateException(LabelGenExceptionEnum exception) : Exception
{
    public readonly LabelGenExceptionEnum Code = exception;
}