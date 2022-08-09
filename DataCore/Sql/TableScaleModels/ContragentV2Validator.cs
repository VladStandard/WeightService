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
		RuleFor(item => ((ContragentV2Entity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
