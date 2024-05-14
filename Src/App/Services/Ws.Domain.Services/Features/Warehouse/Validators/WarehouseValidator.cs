namespace Ws.Domain.Services.Features.Warehouse.Validators;

internal abstract class WarehouseValidator : AbstractValidator<Models.Entities.Ref.Warehouse>
{
    protected WarehouseValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(128);
    }
}