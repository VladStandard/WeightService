namespace Ws.Domain.Services.Features.StorageMethod.Validators;

internal abstract class StorageMethodValidator : AbstractValidator<Models.Entities.Print.StorageMethod>
{
    protected StorageMethodValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}