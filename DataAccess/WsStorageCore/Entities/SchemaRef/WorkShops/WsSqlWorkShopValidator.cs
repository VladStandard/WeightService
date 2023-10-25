namespace WsStorageCore.Entities.SchemaRef.WorkShops;

public sealed class WsSqlWorkShopValidator : WsSqlTableValidator<WsSqlWorkShopEntity>
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
