namespace Ws.Domain.Services.Features.Warehouses.Validators;

internal sealed class WarehouseNewValidator : WarehouseValidator
{
    public WarehouseNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}