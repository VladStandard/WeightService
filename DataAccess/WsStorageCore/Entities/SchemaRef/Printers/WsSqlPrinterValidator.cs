namespace WsStorageCore.Entities.SchemaRef.Printers;

public class WsSqlPrinterValidator : WsSqlTableValidator<WsSqlPrinterEntity>
{
    public WsSqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
    }
}