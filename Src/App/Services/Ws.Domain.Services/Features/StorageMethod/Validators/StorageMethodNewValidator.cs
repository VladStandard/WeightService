namespace Ws.Domain.Services.Features.StorageMethod.Validators;

internal sealed class StorageMethodNewValidator : StorageMethodValidator
{
    public StorageMethodNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}