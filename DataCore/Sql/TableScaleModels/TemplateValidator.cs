// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class TemplateValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public TemplateValidator()
	{
		RuleFor(item => ((TemplateModel)item).Title)
			.NotEmpty()
			.NotNull();
	}
}
