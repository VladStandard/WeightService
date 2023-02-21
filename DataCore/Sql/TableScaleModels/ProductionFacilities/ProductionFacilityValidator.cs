// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.ProductionFacilities;

/// <summary>
/// Table validation "___".
/// </summary>
public class ProductionFacilityValidator : SqlTableValidator<ProductionFacilityModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductionFacilityValidator() : base(false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Address)
            .NotEmpty()
            .NotNull();
    }
}
