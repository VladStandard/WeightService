namespace Ws.Domain.Services.Features.Warehouses.Validators;

internal sealed class WarehouseUpdateValidator : WarehouseValidator
{
    public WarehouseUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}