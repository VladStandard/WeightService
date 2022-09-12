// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public class PluValidator : SqlTableValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluValidator()
    {
	    RuleFor(item => ((PluModel)item).Number)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(100)
		    .LessThanOrEqualTo(999);
		RuleFor(item => ((PluModel)item).Name)
			.NotEmpty()
			.NotNull();
        RuleFor(item => ((PluModel)item).FullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => ((PluModel)item).Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => ((PluModel)item).ShelfLifeDays)
	        .NotNull()
	        .GreaterThanOrEqualTo((short)0)
	        .LessThanOrEqualTo((short)365);
		RuleFor(item => ((PluModel)item).TareWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluModel)item).BoxQuantly)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluModel)item).Gtin)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluModel)item).Ean13)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluModel)item).Itf14)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluModel)item).UpperThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.GreaterThanOrEqualTo(item => ((PluModel)item).LowerThreshold)
			.GreaterThanOrEqualTo(item => ((PluModel)item).NominalWeight);
		RuleFor(item => ((PluModel)item).NominalWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.GreaterThanOrEqualTo(item => ((PluModel)item).LowerThreshold)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluModel)item).UpperThreshold);
		RuleFor(item => ((PluModel)item).LowerThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluModel)item).UpperThreshold)
			.LessThanOrEqualTo(item => ((PluModel)item).NominalWeight);
		RuleFor(item => ((PluModel)item).IsCheckWeight)
			.NotNull();
		RuleFor(item => ((PluModel)item).Template)
			.NotEmpty()
			.NotNull()
			.SetValidator(new TemplateValidator());
		RuleFor(item => ((PluModel)item).Nomenclature)
			.NotEmpty()
			.NotNull()
			.SetValidator(new NomenclatureValidator());
	}
}
