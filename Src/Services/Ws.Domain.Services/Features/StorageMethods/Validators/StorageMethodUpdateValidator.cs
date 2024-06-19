namespace Ws.Domain.Services.Features.StorageMethods.Validators;

internal sealed class StorageMethodUpdateValidator : StorageMethodValidator
{
    public StorageMethodUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}