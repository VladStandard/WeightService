namespace Ws.Domain.Services.Features.Bundle.Validators;

internal sealed class BundleUpdateValidator : BundleValidator
{
    public BundleUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}