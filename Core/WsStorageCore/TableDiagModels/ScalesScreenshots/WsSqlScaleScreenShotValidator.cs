// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table validation "diag.SCALES_SCREENSHOTS".
/// </summary>
public sealed class WsSqlScaleScreenShotValidator : WsSqlTableValidator<WsSqlScaleScreenShotModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlScaleScreenShotValidator() : base(true, true)
    {
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator());
        RuleFor(item => item.ScreenShot)
            .NotEmpty()
            .NotNull();
    }
}