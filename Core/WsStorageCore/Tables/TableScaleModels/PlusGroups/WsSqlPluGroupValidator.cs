namespace WsStorageCore.Tables.TableScaleModels.PlusGroups;

/// <summary>
/// Table validation "PLUS_GROUPS".
/// </summary>
public sealed class WsSqlPluGroupValidator : WsSqlTableValidator<WsSqlPluGroupModel>
{

    public WsSqlPluGroupValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}