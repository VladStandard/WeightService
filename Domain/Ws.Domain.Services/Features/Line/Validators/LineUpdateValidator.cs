namespace Ws.Domain.Services.Features.Line.Validators;

internal sealed class LineUpdateValidator : LineValidator
{
    public LineUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}