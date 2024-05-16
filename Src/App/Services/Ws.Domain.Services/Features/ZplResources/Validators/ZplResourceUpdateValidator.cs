namespace Ws.Domain.Services.Features.ZplResources.Validators;

internal class ZplResourceUpdateValidator : ZplResourceValidator
{
    public ZplResourceUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}