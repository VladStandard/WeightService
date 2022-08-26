// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class HostValidator : BaseValidator
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public HostValidator() : base(ColumnName.Id)
	{
		RuleFor(item => ((HostEntity)item).AccessDt)
			.NotEmpty()
			.NotNull()
			.LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
		RuleFor(item => ((HostEntity)item).Name)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((HostEntity)item).HostName)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((HostEntity)item).Ip)
			.NotEmpty()
			.NotNull();
		// RuleFor(item => ((HostEntity)item).MacAddressValue)
		// 	.NotEmpty()
		// 	.NotNull();
	}
}
