using Ws.Domain.Models.Entities.Ref;
using Ws.StorageCore.Entities.Ref.Printers;
using Ws.StorageCore.Entities.Ref.Warehouses;

namespace Ws.StorageCore.Entities.Ref.Lines;

public sealed class SqlLineValidator : SqlTableValidator<LineEntity>
{
    public SqlLineValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PcName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);
        RuleFor(item => item.Warehouse)
            .SetValidator(new SqlWarehouseValidator(isCheckIdentity));
        RuleFor(item => item.Printer)
            .SetValidator(new SqlPrinterValidator(isCheckIdentity)!);
    }
}