// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class HostValidator : TableValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public HostValidator()
	{
		RuleFor(item => ((HostModel)item).AccessDt)
			.NotEmpty()
			.NotNull()
			.LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
		RuleFor(item => ((HostModel)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((HostModel)item).HostName)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((HostModel)item).Ip)
			.NotEmpty()
			.NotNull();
		// RuleFor(item => ((HostEntity)item).MacAddressValue)
		// 	.NotEmpty()
		// 	.NotNull();
	}
}
