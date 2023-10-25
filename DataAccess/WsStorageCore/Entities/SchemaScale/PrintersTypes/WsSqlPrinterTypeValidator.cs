namespace WsStorageCore.Entities.SchemaScale.PrintersTypes;

/// <summary>
/// Table validation "ZebraPrinterType".
/// </summary>
public sealed class WsSqlPrinterTypeValidator : WsSqlTableValidator<WsSqlPrinterTypeEntity>
{

    public WsSqlPrinterTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
