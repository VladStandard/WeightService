// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleFkModels.PlusBrandsFks;

/// <summary>
/// Table validation "PLUS_BRANDS_FK".
/// </summary>
public sealed class WsSqlPluBrandFkValidator : WsSqlTableValidator<WsSqlPluBrandFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlPluBrandFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Brand)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlBrandValidator(isCheckIdentity));
    }
}