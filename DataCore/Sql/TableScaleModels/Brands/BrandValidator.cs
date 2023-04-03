// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.Brands;

/// <summary>
/// Table validation "BRANDS".
/// </summary>
public sealed class BrandValidator : SqlTableValidator<BrandModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BrandValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}
