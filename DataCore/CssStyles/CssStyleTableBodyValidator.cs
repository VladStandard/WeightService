// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Enums;

namespace DataCore.CssStyles;

public class CssStyleTableBodyValidator : AbstractValidator<CssStyleTableBodyModel>
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Constructor.
	/// </summary>
	public CssStyleTableBodyValidator()
	{
		RuleFor(item => item.IdentityName)
			.NotEqual(SqlFieldIdentityEnum.Empty);
	}

	#endregion
}
