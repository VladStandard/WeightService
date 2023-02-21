// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.TemplatesResources;

namespace DataCore.Sql.TableScaleModels.PrintersResources;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterResourceValidator : SqlTableValidator<PrinterResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PrinterResourceValidator() : base(false, false)
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
            .SetValidator(new TemplateResourceValidator());
    }
}
