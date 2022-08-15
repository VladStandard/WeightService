// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "ORDERS_WEIGHINGS".
/// </summary>
public class OrderWeighingValidator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public OrderWeighingValidator()
	{
		RuleFor(item => item.CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => item.IdentityUid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty);
		RuleFor(item => ((OrderWeighingEntity)item).Order)
			.NotEmpty()
			.NotNull()
			.SetValidator(new OrderValidator());
		RuleFor(item => ((OrderWeighingEntity)item).Fact)
			.NotEmpty()
			.NotNull();
	}
}
