// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "CONTRAGENTS_V2".
/// </summary>
public class ContragentV2Validator : AbstractValidator<ContragentV2Entity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ContragentV2Validator()
	{
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
	}
}
