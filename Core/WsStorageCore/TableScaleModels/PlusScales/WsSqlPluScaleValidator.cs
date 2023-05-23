// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusScales;

/// <summary>
/// Table validation "PLUS_SCALES".
/// </summary>
public sealed class WsSqlPluScaleValidator : WsSqlTableValidator<WsSqlPluScaleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluScaleValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Line)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator());
    }
}
