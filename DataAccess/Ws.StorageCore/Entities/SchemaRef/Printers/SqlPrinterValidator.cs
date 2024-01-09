using Ws.Shared.Validators;

namespace Ws.StorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterValidator : SqlTableValidator<SqlPrinterEntity>
{
    public SqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Ip).MustBeAValidIpAddress();
    }
}