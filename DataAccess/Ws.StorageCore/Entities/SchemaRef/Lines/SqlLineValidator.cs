using Ws.StorageCore.Entities.SchemaRef.Printers;
using Ws.StorageCore.Entities.SchemaRef.Warehouses;

namespace Ws.StorageCore.Entities.SchemaRef.Lines;

public sealed class SqlLineValidator : SqlTableValidator<SqlLineEntity>
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