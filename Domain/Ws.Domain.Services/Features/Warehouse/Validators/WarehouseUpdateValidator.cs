namespace Ws.Domain.Services.Features.Warehouse.Validators;

internal sealed class WarehouseUpdateValidator : WarehouseValidator
{
    public WarehouseUpdateValidator() : base()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}