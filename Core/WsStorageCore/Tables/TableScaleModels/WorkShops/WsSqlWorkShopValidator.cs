// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
