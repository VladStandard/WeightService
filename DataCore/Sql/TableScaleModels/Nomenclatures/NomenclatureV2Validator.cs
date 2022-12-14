// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels.Nomenclatures;

/// <summary>
/// Table validation "NOMENCLATURES".
/// </summary>
public class NomenclatureV2Validator : SqlTableValidator<NomenclatureV2Model>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclatureV2Validator()
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
        RuleFor(item => item.FullName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Code)
            .NotEmpty()
            .NotNull();
    }
}
