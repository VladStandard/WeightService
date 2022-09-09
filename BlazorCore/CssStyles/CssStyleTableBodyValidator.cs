// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;
using FluentValidation;

namespace BlazorCore.CssStyles;

public class CssStyleTableBodyValidator : AbstractValidator<CssStyleBase>
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleTableBodyValidator()
	{
		RuleFor(item => ((CssStyleTableBodyModel)item).IdentityName)
			.NotEqual(SqlFieldIdentityEnum.Empty);
	}

	#endregion
}
