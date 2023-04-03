// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusWeighingsFks;
using DataCore.Sql.TableScaleModels.Orders;

namespace DataCore.Sql.TableScaleModels.OrdersWeighings;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public sealed class OrderWeighingValidator : SqlTableValidator<OrderWeighingModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrderWeighingValidator() : base(true, true)
    {
        RuleFor(item => item.Order)
            .NotEmpty()
            .NotNull()
            .SetValidator(new OrderValidator());
        RuleFor(item => item.PluWeighing)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluWeighingValidator());
    }
}