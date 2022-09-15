// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "___".
/// </summary>
public class PrinterResourceValidator : SqlTableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterResourceValidator() : base(false, false)
	{
		RuleFor(item => ((PrinterResourceModel)item).Description)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PrinterResourceModel)item).Printer)
			.NotEmpty()
			.NotNull()
			.SetValidator(new PrinterValidator());
		RuleFor(item => ((PrinterResourceModel)item).TemplateResource)
			.NotEmpty()
			.NotNull()
			.SetValidator(new TemplateResourceValidator());
	}
}
