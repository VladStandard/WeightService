// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;

namespace BlazorCore.CssStyles;

public class TableHeadStyleValidator : AbstractValidator<TableStyleModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public TableHeadStyleValidator()
	{
		RuleFor(item => ((TableHeadStyleModel)item).ColumnsWidths)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TableHeadStyleModel)item).ColumnsTitles)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TableHeadStyleModel)item).Color)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TableHeadStyleModel)item).FontWeight)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TableHeadStyleModel)item).TextAlign)
			.NotEmpty()
			.NotNull();
	}
}
