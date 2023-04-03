// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableScaleModels.ProductSeries;

/// <summary>
/// Table validation "ProductSeries".
/// </summary>
public sealed class ProductSeriesValidator : SqlTableValidator<ProductSeriesModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ProductSeriesValidator() : base(true, false)
    {
        RuleFor(item => item.Sscc)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ScaleValidator());
    }
}
