namespace WsStorageCore.Tables.TableScaleModels.ProductSeries;

/// <summary>
/// Table validation "ProductSeries".
/// </summary>
public sealed class WsSqlProductSeriesValidator : WsSqlTableValidator<WsSqlProductSeriesModel>
{

    public WsSqlProductSeriesValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
    {
        RuleFor(item => item.Sscc)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator(isCheckIdentity));
    }
}