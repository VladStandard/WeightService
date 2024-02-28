using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.Warehouse.Validators;

internal abstract class WarehouseValidator : AbstractValidator<WarehouseEntity>
{
    protected WarehouseValidator()
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(128);
    }
}
