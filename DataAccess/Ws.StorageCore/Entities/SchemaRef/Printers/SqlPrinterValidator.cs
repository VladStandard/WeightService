namespace Ws.StorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterValidator : SqlTableValidator<SqlPrinterEntity>
{
    public SqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Ip)
            .NotNull()
            .Matches(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$");
    }
}