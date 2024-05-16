namespace Ws.Domain.Services.Features.Brands.Validators;

internal sealed class BrandUpdateValidator : BrandValidator
{
    public BrandUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}