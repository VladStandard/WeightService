// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table validation "PLUS_CHARACTERISTICS_FK".
/// </summary>
public sealed class WsSqlPluCharacteristicsFkValidator : WsSqlTableValidator<WsSqlPluCharacteristicsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluCharacteristicsFkValidator() : base(true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator());
        RuleFor(item => item.Characteristic)
            .NotEmpty()
            .NotNull()
            .SetValidator(new PluCharacteristicValidator());
    }
}