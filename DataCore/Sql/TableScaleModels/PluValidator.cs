// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public class PluValidator : SqlTableValidator<PluModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluValidator()
    {
	    RuleFor(item => item.Number)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(100)
		    .LessThanOrEqualTo(999);
		RuleFor(item => item.Name)
			.NotEmpty()
			.NotNull();
        RuleFor(item => item.FullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
	        .NotNull()
	        .GreaterThanOrEqualTo((short)0)
	        .LessThanOrEqualTo((short)365);
		RuleFor(item => item.TareWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => item.BoxQuantly)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => item.Gtin)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => item.Ean13)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => item.Itf14)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => item.UpperThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.GreaterThanOrEqualTo(item => item.LowerThreshold)
			.GreaterThanOrEqualTo(item => item.NominalWeight);
		RuleFor(item => item.NominalWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.GreaterThanOrEqualTo(item => item.LowerThreshold)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => item.UpperThreshold);
		RuleFor(item => item.LowerThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => item.UpperThreshold)
			.LessThanOrEqualTo(item => item.NominalWeight);
		RuleFor(item => item.IsCheckWeight)
			.NotNull();
		RuleFor(item => item.Template)
			.NotEmpty()
			.NotNull()
			.SetValidator(new TemplateValidator());
		RuleFor(item => item.Nomenclature)
			.NotEmpty()
			.NotNull()
			.SetValidator(new NomenclatureValidator());
	}
}
