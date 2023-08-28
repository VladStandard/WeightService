namespace WsStorageCore.Tables.TableScaleModels.Printers;

public sealed class WsSqlPrinterValidator : WsSqlTableValidator<WsSqlPrinterModel>
{
    public WsSqlPrinterValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.DarknessLevel)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.PrinterType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPrinterTypeValidator(isCheckIdentity));
    }
}
