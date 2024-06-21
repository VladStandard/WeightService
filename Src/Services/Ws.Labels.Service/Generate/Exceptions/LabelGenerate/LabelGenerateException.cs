namespace Ws.Labels.Service.Generate.Exceptions.LabelGenerate;

public class LabelGenerateException(LabelGenExceptions exception) : Exception
{
    public readonly LabelGenExceptions Code = exception;
}