// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "Hosts".
/// </summary>
public class NomenclatureValidator : AbstractValidator<BaseEntity>
{
	/// <summary>
	/// Constructor.
	/// </summary>
	public NomenclatureValidator()
	{
		RuleFor(item => ((NomenclatureEntity)item).CreateDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((NomenclatureEntity)item).ChangeDt)
			.NotEmpty()
			.NotNull()
			.GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
		RuleFor(item => ((NomenclatureEntity)item).IdentityId)
			.NotEmpty()
			.NotNull()
			.NotEqual(0);
		RuleFor(item => ((NomenclatureEntity)item).Name)
			.NotNull();
		RuleFor(item => ((NomenclatureEntity)item).Xml)
			.NotEmpty()
			.NotNull();
	}
}
