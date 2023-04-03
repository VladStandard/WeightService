// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Templates;

/// <summary>
/// Table validation "Templates".
/// </summary>
public sealed class TemplateValidator : SqlTableValidator<TemplateModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateValidator() : base(true, true)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
