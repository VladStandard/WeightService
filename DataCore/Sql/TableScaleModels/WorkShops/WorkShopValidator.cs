// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ProductionFacilities;

namespace DataCore.Sql.TableScaleModels.WorkShops;

/// <summary>
/// Table validation "WorkShop".
/// </summary>
public class WorkShopValidator : SqlTableValidator<WorkShopModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WorkShopValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.ProductionFacility)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ProductionFacilityValidator());
    }
}
