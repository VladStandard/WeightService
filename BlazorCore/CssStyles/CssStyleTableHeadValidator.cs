// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentValidation;

namespace BlazorCore.CssStyles;

public class CssStyleTableHeadValidator : AbstractValidator<CssStyleBase>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleTableHeadValidator()
	{
		RuleFor(item => ((CssStyleTableHeadModel)item).ColumnsWidths)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((CssStyleTableHeadModel)item).ColumnsTitles)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((CssStyleTableHeadModel)item).Color)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((CssStyleTableHeadModel)item).FontWeight)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((CssStyleTableHeadModel)item).TextAlign)
			.NotEmpty()
			.NotNull();
	}
}
