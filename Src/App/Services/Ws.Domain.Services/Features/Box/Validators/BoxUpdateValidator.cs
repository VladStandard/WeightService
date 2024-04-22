namespace Ws.Domain.Services.Features.Box.Validators;

internal sealed class BoxUpdateValidator : BoxValidator
{
    public BoxUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}