namespace Ws.Domain.Services.Features.ProductionSite.Validators;

internal abstract class ProductionSiteValidator : AbstractValidator<Models.Entities.Ref.ProductionSite>
{
    protected ProductionSiteValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150);
        RuleFor(item => item.Address)
            .NotEmpty()
            .MaximumLength(512);
    }
}