namespace Ws.Domain.Services.Features.StorageMethod.Validators;

internal sealed class StorageMethodUpdateValidator : StorageMethodValidator
{
    public StorageMethodUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}