// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "CONTRAGENTS_V2".
/// </summary>
public class ContragentValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public ContragentValidator()
	{
		RuleFor(item => ((ContragentModel)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
