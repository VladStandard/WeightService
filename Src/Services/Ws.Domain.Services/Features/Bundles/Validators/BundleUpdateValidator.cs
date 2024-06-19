namespace Ws.Domain.Services.Features.Bundles.Validators;

internal sealed class BundleUpdateValidator : BundleValidator
{
    public BundleUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}