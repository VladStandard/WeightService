// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table validation "diag.SCALES_SCREENSHOTS".
/// </summary>
public sealed class WsSqlScaleScreenShotValidator : WsSqlTableValidator<WsSqlScaleScreenShotModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
    public WsSqlScaleScreenShotValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator(isCheckIdentity));
        RuleFor(item => item.ScreenShot)
            .NotEmpty()
            .NotNull();
    }
}