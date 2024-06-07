namespace Ws.Domain.Services.Features.ZplResources.Validators;

internal class ZplResourceNewValidator : ZplResourceValidator
{
    public ZplResourceNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}