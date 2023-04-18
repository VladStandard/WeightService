// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsBlazorCore.CssStyles;

public class CssStyleTableHeadValidator : AbstractValidator<CssStyleTableHeadModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleTableHeadValidator()
	{
		RuleFor(item => item.ColumnsWidths)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.ColumnsTitles)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Color)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.FontWeight)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.TextAlign)
			.NotEmpty()
			.NotNull();
	}
}
