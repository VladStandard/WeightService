// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Templates;

/// <summary>
/// Table validation "Templates".
/// </summary>
public sealed class WsSqlTemplateValidator : WsSqlTableValidator<WsSqlTemplateModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlTemplateValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Title)
            .NotEmpty()
            .NotNull();
    }
}
