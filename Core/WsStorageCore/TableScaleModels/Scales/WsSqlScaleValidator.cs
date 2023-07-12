// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Scales;

/// <summary>
/// Валидатор таблицы SCALES.
/// </summary>
public sealed class WsSqlScaleValidator : WsSqlTableValidator<WsSqlScaleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
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
            .SetValidator(new WsSqlWorkShopValidator(isCheckIdentity)!);
        RuleFor(item => item.PrinterMain)
            .SetValidator(new WsSqlPrinterValidator(isCheckIdentity)!);
        RuleFor(item => item.PrinterShipping)
            .SetValidator(new WsSqlPrinterValidator(isCheckIdentity)!);
    }
}