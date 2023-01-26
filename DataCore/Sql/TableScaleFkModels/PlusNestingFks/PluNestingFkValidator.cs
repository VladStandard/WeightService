// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Boxes;

namespace DataCore.Sql.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Table validation "PLUS_NESTING_FK".
/// </summary>
public class PluNestingFkValidator : SqlTableValidator<PluNestingFkModel>
{
    public PluNestingFkValidator() : base(true, true)
    {
        RuleFor(item => item.PluBundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluBundleFkValidator());
        RuleFor(item => item.BundleCount)
            .NotNull();
        RuleFor(item => item.Box)
            .NotEmpty()
            .NotNull()
            .SetValidator(new BoxValidator());
        RuleFor(item => item.WeightMax)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightNom)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMin)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100);
        RuleFor(item => item.WeightMax)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .GreaterThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item.WeightNom > 0 && item.WeightMin > 0);
        RuleFor(item => item.WeightNom)
            .GreaterThanOrEqualTo(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .When(item => item.WeightMax > 0 && item.WeightNom > 0 && item.WeightMin > 0);
        RuleFor(item => item.WeightMin)
            .LessThanOrEqualTo(item => item.WeightMax)
            .LessThanOrEqualTo(item => item.WeightNom)
            .When(item => item.WeightMax > 0 && item.WeightNom > 0 && item.WeightMin > 0);
    }
}