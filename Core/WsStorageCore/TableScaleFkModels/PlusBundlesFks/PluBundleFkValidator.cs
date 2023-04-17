// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Plus;

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table validation "PLUS_BUNDLES_FK".
/// </summary>
public sealed class PluBundleFkValidator : WsSqlTableValidator<PluBundleFkModel>
{    
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluBundleFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BundleValidator());
    }
}