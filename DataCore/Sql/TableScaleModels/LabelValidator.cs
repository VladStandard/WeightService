// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class LabelValidator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public LabelValidator()
	{
		RuleFor(item => item.CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		//RuleFor(item => ((LabelEntity)item).ChangeDt)
		//	.NotEmpty()
		//	.NotNull()
		//	.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.IdentityId)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((LabelEntity)item).WeithingFact)
			.NotEmpty()
			.NotNull();
	}
}
