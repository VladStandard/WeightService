// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Bundles;

/// <summary>
/// Table validation "BUNDLES".
/// </summary>
public sealed class WsSqlBundleValidator : WsSqlTableValidator<WsSqlBundleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlBundleValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Weight)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
    }
}