// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace DataCore.Sql.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table validation "diag.SCALES_SCREENSHOTS".
/// </summary>
public sealed class ScaleScreenShotValidator : WsSqlTableValidator<ScaleScreenShotModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ScaleScreenShotValidator() : base(true, true)
    {
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new ScaleValidator());
        RuleFor(item => item.ScreenShot)
            .NotEmpty()
            .NotNull();
    }
}