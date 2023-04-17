// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;
using WsStorageCore.TableScaleModels.ProductionFacilities;

namespace WsStorageCore.TableScaleModels.WorkShops;

/// <summary>
/// Table validation "WorkShop".
/// </summary>
public sealed class WorkShopValidator : WsSqlTableValidator<WorkShopModel>
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
