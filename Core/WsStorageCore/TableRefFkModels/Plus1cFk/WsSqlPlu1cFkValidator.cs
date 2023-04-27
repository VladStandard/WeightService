// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.TableRefFkModels.Plus1cFk;

/// <summary>
/// Table validation "REF.PLUS_1C_FK".
/// </summary>
public sealed class WsSqlPlu1cFkValidator : WsSqlTableValidator<WsSqlPlu1cFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPlu1cFkValidator() : base(false, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
    }
}