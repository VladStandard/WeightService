namespace WsStorageCore.Tables.TableScaleModels.Apps;

/// <summary>
/// Table validation "APPS".
/// </summary>
public sealed class WsSqlAppValidator : WsSqlTableValidator<WsSqlAppModel>
{

    public WsSqlAppValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}