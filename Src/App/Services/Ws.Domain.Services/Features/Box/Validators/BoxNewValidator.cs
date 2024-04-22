namespace Ws.Domain.Services.Features.Box.Validators;

internal sealed class BoxNewValidator : BoxValidator
{
    public BoxNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}