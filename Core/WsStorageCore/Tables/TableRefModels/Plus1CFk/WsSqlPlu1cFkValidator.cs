// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableRefModels.Plus1CFk;

/// <summary>
/// Валидатор таблицы REF.PLUS_1C_FK.
/// </summary>
public sealed class WsSqlPlu1CFkValidator : WsSqlTableValidator<WsSqlPlu1CFkModel>
{

    public WsSqlPlu1CFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
    }
}