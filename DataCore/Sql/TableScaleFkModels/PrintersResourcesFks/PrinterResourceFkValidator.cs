// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;

/// <summary>
/// Table validation "ZebraPrinterResourceRef".
/// </summary>
public class PrinterResourceFkValidator : SqlTableValidator<PrinterResourceFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterResourceFkValidator() : base(false, false)
    {
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Printer)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PrinterValidator());
        RuleFor(item => item.TemplateResource)
            .NotEmpty()
            .NotNull()
            .SetValidator(new TemplateResourceDeprecatedValidator());
    }
}
