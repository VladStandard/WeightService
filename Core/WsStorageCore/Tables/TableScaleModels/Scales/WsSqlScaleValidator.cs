using WsStorageCore.Tables.TableRefModels.WorkShops;

namespace WsStorageCore.Tables.TableScaleModels.Scales;

public sealed class WsSqlScaleValidator : WsSqlTableValidator<WsSqlScaleModel>
{
    public WsSqlScaleValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Number)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(10000)
            .LessThanOrEqualTo(99999);
        RuleFor(item => item.WorkShop)
            .SetValidator(new WsSqlWorkShopValidator(isCheckIdentity));
        RuleFor(item => item.Device)
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
        RuleFor(item => item.Printer)
            .SetValidator(new WsSqlPrinterValidator(isCheckIdentity)!);
    }
}