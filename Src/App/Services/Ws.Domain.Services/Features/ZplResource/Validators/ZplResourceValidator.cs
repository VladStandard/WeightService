namespace Ws.Domain.Services.Features.ZplResource.Validators;

internal abstract class ZplResourceValidator : AbstractValidator<Models.Entities.Print.ZplResource>
{
    protected ZplResourceValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}