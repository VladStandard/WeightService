namespace WsStorageCore.Entities.SchemaRef.Printers;

public class SqlPrinterValidator : SqlTableValidator<SqlPrinterEntity>
{
    public SqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
    }
}