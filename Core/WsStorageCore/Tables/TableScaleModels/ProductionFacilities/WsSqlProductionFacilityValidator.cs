// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
