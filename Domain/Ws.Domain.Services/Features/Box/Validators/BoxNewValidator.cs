namespace Ws.Domain.Services.Features.Box.Validators;

internal sealed class BoxNewValidator : BoxValidator
{
    public BoxNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}