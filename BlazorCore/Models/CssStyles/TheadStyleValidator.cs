// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;

namespace BlazorCore.Models.CssStyles;

public class TheadStyleValidator : AbstractValidator<IBaseStyleModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public TheadStyleValidator()
	{
		RuleFor(item => ((TheadStyleModel)item).ColumnsWidths)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TheadStyleModel)item).ColumnsTitles)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TheadStyleModel)item).Color)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TheadStyleModel)item).FontWeight)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((TheadStyleModel)item).TextAlign)
			.NotEmpty()
			.NotNull();
	}
}
