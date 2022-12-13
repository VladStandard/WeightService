// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleFkModels.PluTemplateFk;

/// <summary>
/// Table validation "PLUS_TEMPLATES_FK".
/// </summary>
public class PluTemplateFkValidator : SqlTableValidator<PluTemplateFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluTemplateFkValidator()
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Template)
            .NotEmpty()
            .NotNull()
            .SetValidator(new TemplateValidator());
    }
}
