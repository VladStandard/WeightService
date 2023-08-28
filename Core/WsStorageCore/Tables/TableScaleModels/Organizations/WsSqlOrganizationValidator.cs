namespace WsStorageCore.Tables.TableScaleModels.Organizations;

/// <summary>
/// Table validation "ORGANIZATIONS".
/// </summary>
public sealed class WsSqlOrganizationValidator : WsSqlTableValidator<WsSqlOrganizationModel>
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
