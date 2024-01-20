using Ws.Domain.Models.Entities.Ref;
using Ws.Shared.Validators;

namespace Ws.StorageCore.Entities.Ref.Printers;

public class SqlPrinterValidator : SqlTableValidator<PrinterEntity>
{
    public SqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Ip).MustBeAValidIpAddress();
    }
}