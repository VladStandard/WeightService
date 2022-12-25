// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Bundles;

/// <summary>
/// Table validation "BUNDLES".
/// </summary>
public class BundleValidator : SqlTableValidator<BundleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BundleValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotEmpty()
            .NotNull();
    }
}
