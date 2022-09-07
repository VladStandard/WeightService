// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using FluentValidation;

namespace BlazorCore.CssStyles;

public class TableBodyStyleValidator : AbstractValidator<TableStyleModel>
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public TableBodyStyleValidator()
	{
		RuleFor(item => ((TableBodyStyleModel)item).IdentityName)
			.NotEqual(SqlFieldIdentityEnum.Default);
	}

	#endregion
}
