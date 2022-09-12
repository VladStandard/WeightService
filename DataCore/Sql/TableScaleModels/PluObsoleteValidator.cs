//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Tables;

//namespace DataCore.Sql.TableScaleModels;

///// <summary>
///// Table validation "PLUS".
///// </summary>
//public class PluObsoleteValidator : SqlTableValidator
//{
//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public PluObsoleteValidator()
//    {
//	    RuleFor(item => ((PluObsoleteModel)item).PluNumber)
//		    .NotEmpty()
//		    .NotNull()
//		    .GreaterThanOrEqualTo(100)
//		    .LessThanOrEqualTo(999);
//		RuleFor(item => ((PluObsoleteModel)item).GoodsDescription)
//			.NotEmpty()
//			.NotNull();
//        RuleFor(item => ((PluObsoleteModel)item).GoodsFullName)
//            .NotEmpty()
//            .NotNull();
//        RuleFor(item => ((PluObsoleteModel)item).GoodsShelfLifeDays)
//	        .NotNull()
//	        .GreaterThanOrEqualTo((short)0)
//	        .LessThanOrEqualTo((short)365);
//		RuleFor(item => ((PluObsoleteModel)item).GoodsTareWeight)
//			.NotNull()
//			.GreaterThanOrEqualTo(0)
//			.LessThanOrEqualTo(100);
//		RuleFor(item => ((PluObsoleteModel)item).GoodsBoxQuantly)
//			.NotNull()
//			.GreaterThanOrEqualTo(0)
//			.LessThanOrEqualTo(100);
//		RuleFor(item => ((PluObsoleteModel)item).Gtin)
//			//.NotEmpty()
//			.NotNull();
//		RuleFor(item => ((PluObsoleteModel)item).Ean13)
//			//.NotEmpty()
//			.NotNull();
//		RuleFor(item => ((PluObsoleteModel)item).Itf14)
//			//.NotEmpty()
//			.NotNull();
//		RuleFor(item => ((PluObsoleteModel)item).UpperWeightThreshold)
//			.NotNull()
//			.GreaterThanOrEqualTo(0)
//			.LessThanOrEqualTo(100)
//			.GreaterThanOrEqualTo(item => ((PluObsoleteModel)item).LowerWeightThreshold)
//			.GreaterThanOrEqualTo(item => ((PluObsoleteModel)item).NominalWeight);
//		RuleFor(item => ((PluObsoleteModel)item).NominalWeight)
//			.NotNull()
//			.GreaterThanOrEqualTo(0)
//			.GreaterThanOrEqualTo(item => ((PluObsoleteModel)item).LowerWeightThreshold)
//			.LessThanOrEqualTo(100)
//			.LessThanOrEqualTo(item => ((PluObsoleteModel)item).UpperWeightThreshold);
//		RuleFor(item => ((PluObsoleteModel)item).LowerWeightThreshold)
//			.NotNull()
//			.GreaterThanOrEqualTo(0)
//			.LessThanOrEqualTo(100)
//			.LessThanOrEqualTo(item => ((PluObsoleteModel)item).UpperWeightThreshold)
//			.LessThanOrEqualTo(item => ((PluObsoleteModel)item).NominalWeight);
//		RuleFor(item => ((PluObsoleteModel)item).IsCheckWeight)
//			.NotNull();
//		RuleFor(item => ((PluObsoleteModel)item).Scale)
//			.NotEmpty()
//			.NotNull()
//			.SetValidator(new ScaleValidator());
//		RuleFor(item => ((PluObsoleteModel)item).Template)
//			.NotEmpty()
//			.NotNull()
//			.SetValidator(new TemplateValidator());
//		RuleFor(item => ((PluObsoleteModel)item).Nomenclature)
//			.NotEmpty()
//			.NotNull()
//			.SetValidator(new NomenclatureValidator());
//	}
//}
