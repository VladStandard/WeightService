// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table validation "ProductionFacility".
/// </summary>
public sealed class WsSqlProductionFacilityValidator : WsSqlTableValidator<WsSqlProductionFacilityModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlProductionFacilityValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .NotNull();
    }
}
