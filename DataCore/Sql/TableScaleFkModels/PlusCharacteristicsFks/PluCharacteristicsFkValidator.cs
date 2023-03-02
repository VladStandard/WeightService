// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;

namespace DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table validation "PLUS_CHARACTERISTICS_FK".
/// </summary>
public class PluCharacteristicsFkValidator : SqlTableValidator<PluCharacteristicsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluCharacteristicsFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluValidator());
        RuleFor(item => item.Characteristic)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluCharacteristicValidator());
    }
}