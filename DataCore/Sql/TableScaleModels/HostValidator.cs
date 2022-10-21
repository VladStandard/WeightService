// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class HostValidator : SqlTableValidator<HostModel>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public HostValidator()
	{
		RuleFor(item => item.LoginDt)
			.NotEmpty()
			.NotNull()
			.LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.HostName)
			.NotEmpty()
			.NotNull();
		RuleFor(item => item.Ip)
			.NotEmpty()
			.NotNull();
		// RuleFor(item => (item).MacAddressValue)
		// 	.NotEmpty()
		// 	.NotNull();
	}
}
