namespace WsStorageCore.Tables.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table validation "ProductionFacility".
/// </summary>
public sealed class WsSqlProductionFacilityValidator : WsSqlTableValidator<WsSqlProductionFacilityModel>
{

    public WsSqlProductionFacilityValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .NotNull();
    }
}
