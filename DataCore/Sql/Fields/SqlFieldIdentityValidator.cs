// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Fields;

public class SqlFieldIdentityValidator : AbstractValidator<SqlFieldIdentityModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlFieldIdentityValidator()
	{
		RuleFor(item => item.Id)
			.NotEmpty()
			.NotNull()
			.NotEqual(0).When(item => item.Name == ColumnName.Id);
		RuleFor(item => item.Uid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty).When(item => item.Name == ColumnName.Uid);
	}
}
