using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.StorageMethod.Validators;

internal abstract class StorageMethodValidator : AbstractValidator<StorageMethodEntity>
{
    protected StorageMethodValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}