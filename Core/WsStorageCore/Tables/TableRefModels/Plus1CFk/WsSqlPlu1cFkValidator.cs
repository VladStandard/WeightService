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