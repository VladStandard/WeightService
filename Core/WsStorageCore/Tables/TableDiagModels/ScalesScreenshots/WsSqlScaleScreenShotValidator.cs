namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table validation "diag.SCALES_SCREENSHOTS".
/// </summary>
public sealed class WsSqlScaleScreenShotValidator : WsSqlTableValidator<WsSqlScaleScreenShotModel>
{

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