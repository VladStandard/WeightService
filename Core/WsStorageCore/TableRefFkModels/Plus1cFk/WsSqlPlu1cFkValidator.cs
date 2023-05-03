// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableRefFkModels.Plus1CFk;

/// <summary>
/// Table validation "REF.PLUS_1C_FK".
/// </summary>
public sealed class WsSqlPlu1CFkValidator : WsSqlTableValidator<WsSqlPlu1CFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPlu1CFkValidator() : base(false, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
    }
}