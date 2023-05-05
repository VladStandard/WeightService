// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Brands;

/// <summary>
/// Table validation "BRANDS".
/// </summary>
public sealed class WsSqlBrandValidator : WsSqlTableValidator<WsSqlBrandModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlBrandValidator() : base(true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}
