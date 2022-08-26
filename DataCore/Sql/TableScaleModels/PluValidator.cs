// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "PLUS".
/// </summary>
public class PluValidator : BaseValidator
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluValidator() : base(ColumnName.Uid)
	{
	    RuleFor(item => ((PluEntity)item).Number)
		    .NotEmpty()
		    .NotNull()
		    .GreaterThanOrEqualTo(100)
		    .LessThanOrEqualTo(999);
		RuleFor(item => ((PluEntity)item).Name)
			.NotEmpty()
			.NotNull();
        RuleFor(item => ((PluEntity)item).FullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => ((PluEntity)item).Description)
            .NotEmpty()
            .NotNull();
        RuleFor(item => ((PluEntity)item).ShelfLifeDays)
	        .NotNull()
	        .GreaterThanOrEqualTo((short)0)
	        .LessThanOrEqualTo((short)365);
		RuleFor(item => ((PluEntity)item).TareWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluEntity)item).BoxQuantly)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100);
		RuleFor(item => ((PluEntity)item).Gtin)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluEntity)item).Ean13)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluEntity)item).Itf14)
			//.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluEntity)item).UpperThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.GreaterThanOrEqualTo(item => ((PluEntity)item).LowerThreshold)
			.GreaterThanOrEqualTo(item => ((PluEntity)item).NominalWeight);
		RuleFor(item => ((PluEntity)item).NominalWeight)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.GreaterThanOrEqualTo(item => ((PluEntity)item).LowerThreshold)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluEntity)item).UpperThreshold);
		RuleFor(item => ((PluEntity)item).LowerThreshold)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(100)
			.LessThanOrEqualTo(item => ((PluEntity)item).UpperThreshold)
			.LessThanOrEqualTo(item => ((PluEntity)item).NominalWeight);
		RuleFor(item => ((PluEntity)item).IsCheckWeight)
			.NotNull();
		RuleFor(item => ((PluEntity)item).Template)
			.NotEmpty()
			.NotNull();
		RuleFor(item => ((PluEntity)item).Nomenclature)
			.NotEmpty()
			.NotNull();
	}
}
