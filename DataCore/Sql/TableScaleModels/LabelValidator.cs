// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LabelValidator : AbstractValidator<LabelEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LabelValidator()
	{
		RuleFor(item => item.WeithingFact)
			.NotEmpty()
			.NotNull();
	}
}
