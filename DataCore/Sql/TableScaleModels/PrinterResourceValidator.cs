// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterResourceValidator : BaseUidValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterResourceValidator()
	{
		RuleFor(item => ((PrinterResourceEntity)item).Description)
			.NotEmpty()
			.NotNull();
	}
}
