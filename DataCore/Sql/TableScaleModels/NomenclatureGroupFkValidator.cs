// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table validation "NOMENCLATURES_GROUPS_FK".
/// </summary>
public class NomenclatureGroupFkValidator : SqlTableValidator<NomenclatureGroupFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureGroupFkValidator()
    {
        RuleFor(item => item.NomenclatureGroup)
            .NotEmpty()
            .NotNull()
	        .SetValidator(new NomenclatureGroupValidator());
		RuleFor(item => item.NomenclatureGroupParent)
            .NotEmpty()
            .NotNull()
            .SetValidator(new NomenclatureGroupValidator());
    }
}
