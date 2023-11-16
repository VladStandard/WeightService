using WsStorageCore.Entities.SchemaRef.Hosts;
using WsStorageCore.Entities.SchemaRef.Printers;

namespace WsStorageCore.Entities.SchemaScale.Scales;

public sealed class SqlScaleValidator : SqlTableValidator<SqlScaleEntity>
{
    public SqlScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
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