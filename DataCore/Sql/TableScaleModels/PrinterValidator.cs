// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterValidator : SqlTableValidator<PrinterModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterValidator()
	{
		RuleFor(item => item.DarknessLevel)
			.NotNull()
			.GreaterThanOrEqualTo((short)0);
		RuleFor(item => item.PrinterType)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PrinterTypeValidator());
	}
}
