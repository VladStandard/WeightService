namespace Ws.Domain.Services.Features.Warehouse.Validators;

internal sealed class WarehouseUpdateValidator : WarehouseValidator
{
    public WarehouseUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}