namespace WsStorageCore.Tables.TableRefModels.ProductionSites;

/// <summary>
/// Table validation "ProductionSite".
/// </summary>
public sealed class WsSqlProductionSiteValidator : WsSqlTableValidator<WsSqlProductionSiteModel>
{

    public WsSqlProductionSiteValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(150)
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .MaximumLength(512)
            .NotNull();
    }
}
