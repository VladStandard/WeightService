// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "CONTRAGENTS_V2".
/// </summary>
public class ContragentV2Validator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ContragentV2Validator()
	{
		RuleFor(item => item.CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.IdentityUid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty);
		RuleFor(item => ((ContragentV2Entity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
