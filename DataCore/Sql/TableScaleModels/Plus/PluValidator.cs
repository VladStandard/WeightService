// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;

namespace DataCore.Sql.TableScaleModels.Plus;

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
            //.NotEmpty()
            .NotNull();
        RuleFor(item => item.ShelfLifeDays)
            .NotNull()
            .GreaterThanOrEqualTo((short)0)
            .LessThanOrEqualTo((short)365);
        //RuleFor(item => item.TareWeight)
        //	.NotNull()
        //	.GreaterThanOrEqualTo(0)
        //	.LessThanOrEqualTo(100);
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
		RuleFor(item => item.IsCheckWeight)
            .NotNull();
        RuleFor(item => item.Nomenclature)
            .NotEmpty()
            .NotNull()
            .SetValidator(new NomenclatureValidator());
        // Fix for 0 values.
        RuleFor(item => item.UpperThreshold)
	        .NotNull()
	        .GreaterThanOrEqualTo(0)
	        .LessThanOrEqualTo(100);
        RuleFor(item => item.NominalWeight)
	        .NotNull()
	        .GreaterThanOrEqualTo(0)
	        .LessThanOrEqualTo(100);
        RuleFor(item => item.LowerThreshold)
	        .NotNull()
	        .GreaterThanOrEqualTo(0)
	        .LessThanOrEqualTo(100);
        RuleFor(item => item.UpperThreshold)
	        .GreaterThanOrEqualTo(item => item.LowerThreshold)
	        .GreaterThanOrEqualTo(item => item.NominalWeight)
	        .When(item => item.UpperThreshold > 0 && item.NominalWeight > 0 && item.LowerThreshold > 0);
		RuleFor(item => item.NominalWeight)
	        .GreaterThanOrEqualTo(item => item.LowerThreshold)
	        .LessThanOrEqualTo(item => item.UpperThreshold)
			.When(item => item.UpperThreshold > 0 && item.NominalWeight > 0 && item.LowerThreshold > 0);
		RuleFor(item => item.LowerThreshold)
	        .LessThanOrEqualTo(item => item.UpperThreshold)
	        .LessThanOrEqualTo(item => item.NominalWeight)
			.When(item => item.UpperThreshold > 0 && item.NominalWeight > 0 && item.LowerThreshold > 0);
	}
}
