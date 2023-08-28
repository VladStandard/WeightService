namespace WsStorageCore.Tables.TableScaleModels.WorkShops;

public sealed class WsSqlWorkShopValidator : WsSqlTableValidator<WsSqlWorkShopModel>
{
    public WsSqlWorkShopValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.ProductionFacility)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlProductionFacilityValidator(isCheckIdentity));
    }
}
