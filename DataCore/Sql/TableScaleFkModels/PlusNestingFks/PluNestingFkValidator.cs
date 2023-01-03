// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleFkModels.NestingFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace DataCore.Sql.TableScaleFkModels.PlusNestingFks;

/// <summary>
/// Table validation "PLUS_NESTING_FK".
/// </summary>
public class PluNestingFkValidator : SqlTableValidator<PluNestingFkModel>
{
    public PluNestingFkValidator()
    {
        RuleFor(item => item.PluBundle)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluBundleFkValidator());
        RuleFor(item => item.Nesting)
            .NotEmpty()
            .NotNull()
            .SetValidator(new NestingFkValidator());
    }
}