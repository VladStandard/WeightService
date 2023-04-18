// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table validation "PLUS_CHARACTERISTICS_FK".
/// </summary>
public sealed class PluCharacteristicsFkValidator : WsSqlTableValidator<PluCharacteristicsFkModel>
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