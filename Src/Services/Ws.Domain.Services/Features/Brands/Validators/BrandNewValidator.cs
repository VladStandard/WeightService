namespace Ws.Domain.Services.Features.Brands.Validators;

internal sealed class BrandNewValidator : BrandValidator
{
    public BrandNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}