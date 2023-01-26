// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;

namespace DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;

/// <summary>
/// Table validation "NOMENCLATURES_CHARACTERISTICS_FK".
/// </summary>
public class NomenclaturesCharacteristicsFkValidator : SqlTableValidator<NomenclaturesCharacteristicsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public NomenclaturesCharacteristicsFkValidator() : base(true, true)
    {
        RuleFor(item => item.Nomenclature)
            .NotEmpty()
            .NotNull()
            .SetValidator(new NomenclatureV2Validator());
        RuleFor(item => item.NomenclaturesCharacteristics)
            .NotEmpty()
            .NotNull()
            .SetValidator(new NomenclaturesCharacteristicsValidator());
    }
}