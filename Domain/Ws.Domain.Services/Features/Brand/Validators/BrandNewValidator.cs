namespace Ws.Domain.Services.Features.Brand.Validators;

internal sealed class BrandNewValidator : BrandValidator
{
    public BrandNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}