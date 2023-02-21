// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table validation "PLUS_BRANDS_FK".
/// </summary>
public class PluBrandFkValidator : SqlTableValidator<PluBrandFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBrandFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Brand)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BrandValidator());
    }
}