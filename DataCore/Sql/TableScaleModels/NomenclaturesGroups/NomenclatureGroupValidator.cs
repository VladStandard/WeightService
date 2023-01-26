// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.NomenclaturesGroups;

/// <summary>
/// Table validation "NOMENCLATURES_GROUPS".
/// </summary>
public class NomenclatureGroupValidator : SqlTableValidator<NomenclatureGroupModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureGroupValidator() : base(true, true)
    {
        RuleFor(item => item.CreateDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.ChangeDt)
            .NotEmpty()
            .NotNull()
            .GreaterThanOrEqualTo(new DateTime(2020, 01, 01));
        RuleFor(item => item.Name)
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}
