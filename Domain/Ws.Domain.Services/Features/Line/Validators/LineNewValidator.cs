namespace Ws.Domain.Services.Features.Line.Validators;

internal sealed class LineNewValidator : LineValidator
{
    public LineNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}