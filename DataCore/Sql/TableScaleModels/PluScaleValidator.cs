// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS_SCALES".
/// </summary>
public class PluScaleValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluScaleValidator() : base(ColumnName.Uid)
	{
		RuleFor(item => ((PluScaleEntity)item).Plu)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PluValidator());
		RuleFor(item => ((PluScaleEntity)item).Scale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ScaleValidator());
	}
}
