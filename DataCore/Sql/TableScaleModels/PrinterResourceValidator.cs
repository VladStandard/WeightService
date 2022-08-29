// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterResourceValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterResourceValidator() : base(ColumnName.Id, false, false)
	{
		RuleFor(item => ((PrinterResourceEntity)item).Description)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PrinterResourceEntity)item).Printer)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PrinterValidator());
		RuleFor(item => ((PrinterResourceEntity)item).Resource)
			.NotEmpty()
			.NotNull()
			.SetValidator(new TemplateResourceValidator());
	}
}
