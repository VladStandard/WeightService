// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "BARCODE_TYPES_V2".
/// </summary>
public class BarCodeTypeV2Validator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public BarCodeTypeV2Validator()
	{
		RuleFor(item => ((BarCodeTypeV2Entity)item).CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((BarCodeTypeV2Entity)item).ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((BarCodeTypeV2Entity)item).IdentityUid)
			.NotEmpty()
			.NotNull()
			.NotEqual(Guid.Empty);
		RuleFor(item => ((BarCodeTypeV2Entity)item).Name)
			.NotEmpty()
			.NotNull();
	}
}
