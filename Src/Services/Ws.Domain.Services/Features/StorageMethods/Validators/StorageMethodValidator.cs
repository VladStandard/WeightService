using Ws.Domain.Models.Entities.Print;

namespace Ws.Domain.Services.Features.StorageMethods.Validators;

internal abstract class StorageMethodValidator : AbstractValidator<StorageMethod>
{
    protected StorageMethodValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty();
        RuleFor(item => item.Zpl)
            .NotEmpty();
    }
}