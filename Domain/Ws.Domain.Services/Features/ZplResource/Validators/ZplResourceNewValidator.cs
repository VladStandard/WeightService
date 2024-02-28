namespace Ws.Domain.Services.Features.ZplResource.Validators;

internal class ZplResourceNewValidator : ZplResourceValidator
{
    public ZplResourceNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }   
}