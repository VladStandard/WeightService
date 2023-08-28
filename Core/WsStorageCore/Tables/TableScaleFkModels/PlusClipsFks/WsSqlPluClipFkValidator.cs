namespace WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

/// <summary>
/// Валидатор таблицы связей клипс и ПЛУ.
/// </summary>
public sealed class WsSqlPluClipFkValidator : WsSqlTableValidator<WsSqlPluClipFkModel>
{
    public WsSqlPluClipFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Clip)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlClipValidator(isCheckIdentity));
    }
}