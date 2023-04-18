// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusGroupsFks;

/// <summary>
/// Table validation "PLUS_GROUPS_FK".
/// </summary>
public sealed class PluGroupFkValidator : WsSqlTableValidator<PluGroupFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupFkValidator() : base(true, true)
    {
        //RuleFor(item => item.Plu)
        //    .SetValidator(new PluValidator())
        //    .When(item => item.Plu is not null);
        RuleFor(item => item.PluGroup)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluGroupValidator());
        RuleFor(item => item.Parent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluGroupValidator());
    }
}