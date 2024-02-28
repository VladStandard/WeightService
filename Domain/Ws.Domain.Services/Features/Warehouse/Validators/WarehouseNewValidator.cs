namespace Ws.Domain.Services.Features.Warehouse.Validators;

internal sealed class WarehouseNewValidator : WarehouseValidator
{
    public WarehouseNewValidator() : base()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}