// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace DataCore.Sql.TableScaleModels.Printers;

/// <summary>
/// Table validation "ZebraPrinter".
/// </summary>
public sealed class PrinterValidator : WsSqlTableValidator<PrinterModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterValidator() : base(true, true)
    {
        RuleFor(item => item.DarknessLevel)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.PrinterType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PrinterTypeValidator());
    }
}
