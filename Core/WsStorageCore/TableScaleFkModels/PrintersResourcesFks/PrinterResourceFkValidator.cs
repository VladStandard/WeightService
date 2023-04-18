// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table validation "ZebraPrinterResourceRef".
/// </summary>
public sealed class PrinterResourceFkValidator : WsSqlTableValidator<PrinterResourceFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterResourceFkValidator() : base(false, false)
    {
        RuleFor(item => item.Printer)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PrinterValidator());
        RuleFor(item => item.TemplateResource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new TemplateResourceValidator());
    }
}