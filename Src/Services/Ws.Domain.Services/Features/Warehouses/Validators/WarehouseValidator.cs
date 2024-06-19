using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouses.Validators;

internal abstract class WarehouseValidator : AbstractValidator<Warehouse>
{
    protected WarehouseValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(128);
    }
}