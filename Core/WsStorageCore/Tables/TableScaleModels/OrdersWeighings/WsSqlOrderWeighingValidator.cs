namespace WsStorageCore.Tables.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public sealed class WsSqlOrderWeighingValidator : WsSqlTableValidator<WsSqlOrderWeighingModel>
{

    public WsSqlOrderWeighingValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Order)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlOrderValidator(isCheckIdentity));
        RuleFor(item => item.PluWeighing)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluWeighingValidator(isCheckIdentity));
    }
}