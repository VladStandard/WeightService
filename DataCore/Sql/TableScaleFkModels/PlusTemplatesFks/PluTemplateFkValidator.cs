// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.Templates;

namespace DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

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
