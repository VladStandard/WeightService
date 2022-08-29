// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using FluentValidation;

namespace BlazorCore.Models.CssStyles;

public class TableBodyStyleValidator : AbstractValidator<IBaseStyleModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public TableBodyStyleValidator()
	{
		RuleFor(item => ((TableBodyStyleModel)item).IdentityName)
			.NotEqual(ColumnName.Default);
	}
}
