namespace Ws.Domain.Services.Features.StorageMethods.Validators;

internal sealed class StorageMethodNewValidator : StorageMethodValidator
{
    public StorageMethodNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}