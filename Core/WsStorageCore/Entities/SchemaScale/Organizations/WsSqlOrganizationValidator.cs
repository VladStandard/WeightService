namespace WsStorageCore.Entities.SchemaScale.Organizations;

/// <summary>
/// Table validation "ORGANIZATIONS".
/// </summary>
public sealed class WsSqlOrganizationValidator : WsSqlTableValidator<WsSqlOrganizationEntity>
{

    public WsSqlOrganizationValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Gln)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
    }
}
