// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
