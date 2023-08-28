namespace WsStorageCore.Tables.TableScaleModels.Orders;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public sealed class WsSqlOrderValidator : WsSqlTableValidator<WsSqlOrderModel>
{

    public WsSqlOrderValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull()
            .Length(1, 256);
        RuleFor(item => item.BoxCount)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(999);
        RuleFor(item => item.PalletCount)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(999);
    }
}
