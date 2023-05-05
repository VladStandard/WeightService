// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Orders;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public sealed class WsSqlOrderValidator : WsSqlTableValidator<WsSqlOrderModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlOrderValidator() : base(true, true)
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
