namespace WsStorageCore.Entities.SchemaScale.Access;

/// <summary>
/// Table validation "ACCESS".
/// </summary>
public sealed class WsSqlAccessValidator : WsSqlTableValidator<WsSqlAccessEntity>
{

    public WsSqlAccessValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2000, 01, 01));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Rights)
            .NotNull()
            .LessThanOrEqualTo((byte)WsEnumAccessRights.Admin)
            .GreaterThanOrEqualTo((byte)WsEnumAccessRights.None);
    }
}
