// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.PlusGroups;

namespace DataCore.Sql.TableScaleFkModels.PlusGroupsFks;

/// <summary>
/// Table validation "PLUS_GROUPS_FK".
/// </summary>
public class PluGroupFkValidator : SqlTableValidator<PluGroupFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluGroupFkValidator() : base(true, true)
    {
        RuleFor(item => item.NomenclatureGroup)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluGroupValidator());
        RuleFor(item => item.NomenclatureGroupParent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluGroupValidator());
    }
}