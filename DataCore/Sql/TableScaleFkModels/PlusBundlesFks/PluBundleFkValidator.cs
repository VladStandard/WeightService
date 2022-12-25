// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table validation "PLUS_BUNDLES_FK".
/// </summary>
public class PluBundleFkValidator : SqlTableValidator<PluBundleFkModel>
{    
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkValidator()
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.BundleFk)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BundleFkValidator());

    }
}