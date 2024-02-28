namespace Ws.Domain.Services.Features.Bundle.Validators;

internal sealed class BundleUpdateValidator : BundleValidator
{
    public BundleUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}