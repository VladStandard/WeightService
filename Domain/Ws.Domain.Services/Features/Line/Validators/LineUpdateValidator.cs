namespace Ws.Domain.Services.Features.Line.Validators;

internal sealed class LineUpdateValidator : LineValidator
{
    public LineUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}