// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;

namespace WsStorageCore.TableScaleModels.TemplatesResources;

/// <summary>
/// Table validation "TEMPLATES_RESOURCES".
/// </summary>
public sealed class TemplateResourceValidator : WsSqlTableValidator<TemplateResourceModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public TemplateResourceValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Type)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.DataValue)
            .NotEmpty()
            .NotNull();
    }
}