//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Tables;
//using DataCore.Sql.TableScaleModels.Packages;
//using DataCore.Sql.TableScaleModels.Plus;

//namespace DataCore.Sql.TableScaleModels.PlusPackages;

///// <summary>
///// Table validation "PLUS_PACKAGES".
///// </summary>
//public class PluPackageValidator : SqlTableValidator<PluPackageModel>
//{
//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public PluPackageValidator()
//    {
//        RuleFor(item => item.Name)
//            .NotEmpty()
//            .NotNull();
//        RuleFor(item => item.Package)
//            .NotEmpty()
//            .NotNull()
//            .SetValidator(new PackageValidator());
//        RuleFor(item => item.Plu)
//            .NotEmpty()
//            .NotNull()
//            .SetValidator(new PluValidator());
//    }
//}
