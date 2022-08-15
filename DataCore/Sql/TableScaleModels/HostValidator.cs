// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class HostValidator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public HostValidator()
	{
		RuleFor(item => item.CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.IdentityId)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
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
