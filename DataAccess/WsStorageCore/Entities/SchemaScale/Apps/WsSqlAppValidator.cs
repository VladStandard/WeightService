namespace WsStorageCore.Entities.SchemaScale.Apps;

/// <summary>
/// Table validation "APPS".
/// </summary>
public sealed class WsSqlAppValidator : WsSqlTableValidator<WsSqlAppEntity>
{

    public WsSqlAppValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}