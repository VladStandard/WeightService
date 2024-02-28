namespace Ws.Domain.Services.Features.ZplResource.Validators;

internal class ZplResourceUpdateValidator : ZplResourceValidator
{
    public ZplResourceUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}