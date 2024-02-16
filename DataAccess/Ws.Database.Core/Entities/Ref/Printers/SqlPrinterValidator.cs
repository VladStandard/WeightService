using Ws.Domain.Models.Entities.Ref;
using Ws.Shared.Validators;

namespace Ws.Database.Core.Entities.Ref.Printers;

public class SqlPrinterValidator : SqlTableValidator<PrinterEntity>
{
    public SqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}