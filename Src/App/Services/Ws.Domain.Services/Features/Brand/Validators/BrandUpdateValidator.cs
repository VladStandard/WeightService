namespace Ws.Domain.Services.Features.Brand.Validators;

internal sealed class BrandUpdateValidator : BrandValidator
{
    public BrandUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}