using WsStorageCore.Tables.TableRefModels.ProductionSites;

namespace WsStorageCore.Tables.TableRefModels.WorkShops;

public sealed class WsSqlWorkShopValidator : WsSqlTableValidator<WsSqlWorkShopModel>
{
    public WsSqlWorkShopValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .MaximumLength(128)
            .NotNull();
        RuleFor(item => item.ProductionSite)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlProductionSiteValidator(isCheckIdentity));
    }
}
