// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_SCALES".
/// </summary>
public class ScaleScreenShotValidator : SqlTableValidator<ScaleScreenShotModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ScaleScreenShotValidator()
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
