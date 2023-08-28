namespace WsStorageCore.Tables.TableScaleModels.PrintersTypes;

/// <summary>
/// Table validation "ZebraPrinterType".
/// </summary>
public sealed class WsSqlPrinterTypeValidator : WsSqlTableValidator<WsSqlPrinterTypeModel>
{

    public WsSqlPrinterTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
