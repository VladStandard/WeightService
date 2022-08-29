// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public class PluObsoleteValidator : TableValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluObsoleteValidator() : base(ColumnName.Id)
	{
	    RuleFor(item => ((PluObsoleteEntity)item).PluNumber)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(100)
		    .LessThanOrEqualTo(999);
		RuleFor(item => ((PluObsoleteEntity)item).GoodsDescription)
			.NotEmpty()
			.NotNull();
        RuleFor(item => ((PluObsoleteEntity)item).GoodsFullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => ((PluObsoleteEntity)item).GoodsShelfLifeDays)
	        .NotNull()
	        .GreaterThanOrEqualTo((short)0)
	        .LessThanOrEqualTo((short)365);
		RuleFor(item => ((PluObsoleteEntity)item).GoodsTareWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluObsoleteEntity)item).GoodsBoxQuantly)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluObsoleteEntity)item).Gtin)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluObsoleteEntity)item).Ean13)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluObsoleteEntity)item).Itf14)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluObsoleteEntity)item).UpperWeightThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.GreaterThanOrEqualTo(item => ((PluObsoleteEntity)item).LowerWeightThreshold)
			.GreaterThanOrEqualTo(item => ((PluObsoleteEntity)item).NominalWeight);
		RuleFor(item => ((PluObsoleteEntity)item).NominalWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.GreaterThanOrEqualTo(item => ((PluObsoleteEntity)item).LowerWeightThreshold)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluObsoleteEntity)item).UpperWeightThreshold);
		RuleFor(item => ((PluObsoleteEntity)item).LowerWeightThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluObsoleteEntity)item).UpperWeightThreshold)
			.LessThanOrEqualTo(item => ((PluObsoleteEntity)item).NominalWeight);
		RuleFor(item => ((PluObsoleteEntity)item).IsCheckWeight)
			.NotNull();
		RuleFor(item => ((PluObsoleteEntity)item).Scale)
			.NotEmpty()
			.NotNull()
			.SetValidator(new ScaleValidator());
		RuleFor(item => ((PluObsoleteEntity)item).Template)
			.NotEmpty()
			.NotNull()
			.SetValidator(new TemplateValidator());
		RuleFor(item => ((PluObsoleteEntity)item).Nomenclature)
			.NotEmpty()
			.NotNull()
			.SetValidator(new NomenclatureValidator());
	}
}
