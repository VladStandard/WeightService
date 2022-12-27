//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore.Sql.Tables;

//namespace DataCore.Sql.TableScaleModels.Packages;

///// <summary>
///// Table validation "PACKAGES".
///// </summary>
//public class PackageValidator : SqlTableValidator<PackageModel>
//{
//    /// <summary>
//    /// Constructor.
//    /// </summary>
//    public PackageValidator()
//    {
//        RuleFor(item => item.Name)
//            .NotEmpty()
//            .NotNull();
//        RuleFor(item => item.Weight)
//            .NotEmpty()
//            .NotNull()
//            .GreaterThan(0);
//    }
//}
