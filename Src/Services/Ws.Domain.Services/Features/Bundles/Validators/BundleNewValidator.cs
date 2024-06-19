namespace Ws.Domain.Services.Features.Bundles.Validators;

internal sealed class BundleNewValidator : BundleValidator
{
    public BundleNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}