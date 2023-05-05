// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusBundlesFks;

/// <summary>
/// Table validation "PLUS_BUNDLES_FK".
/// </summary>
public sealed class WsSqlPluBundleFkValidator : WsSqlTableValidator<WsSqlPluBundleFkModel>
{    
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluBundleFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Bundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlBundleValidator());
    }
}