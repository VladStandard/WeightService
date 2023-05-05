// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusFks;

/// <summary>
/// Table validation "PLUS_FK".
/// </summary>
public sealed class PluFkValidator : WsSqlTableValidator<PluFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Parent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
    }
}