// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public sealed class WsSqlOrderWeighingValidator : WsSqlTableValidator<WsSqlOrderWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrderWeighingValidator() : base(true, true)
    {
        RuleFor(item => item.Order)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlOrderValidator());
        RuleFor(item => item.PluWeighing)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluWeighingValidator());
    }
}