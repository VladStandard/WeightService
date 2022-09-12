// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterTypeValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterTypeValidator() : base(false, false)
	{
		RuleFor(item => ((PrinterTypeModel)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
