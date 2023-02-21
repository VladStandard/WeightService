// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.TemplatesResources;

/// <summary>
/// Table validation "___".
/// </summary>
public class TemplateResourceValidator : SqlTableValidator<TemplateResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
    }
}
