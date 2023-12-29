using Ws.StorageCore.Entities.SchemaRef.Hosts;
using Ws.StorageCore.Entities.SchemaRef.Printers;

namespace Ws.StorageCore.Entities.SchemaRef.Lines;

public sealed class SqlLineValidator : SqlTableValidator<SqlLineEntity>
{
    public SqlLineValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);
        RuleFor(item => item.WorkShop)
            .SetValidator(new SqlWorkShopValidator(isCheckIdentity));
        RuleFor(item => item.Host)
            .SetValidator(new SqlHostValidator(isCheckIdentity));
        RuleFor(item => item.Printer)
            .SetValidator(new SqlPrinterValidator(isCheckIdentity)!);
    }
}