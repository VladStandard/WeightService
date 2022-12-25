// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;

namespace DataCore.Sql.TableScaleFkModels.BundlesFks;

/// <summary>
/// Table validation "BUNDLES_FK".
/// </summary>
public class BundleFkValidator : SqlTableValidator<BundleFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BundleFkValidator()
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.BundleCount)
            .NotNull();
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BundleValidator());
        RuleFor(item => item.Box)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BoxValidator());
    }
}

