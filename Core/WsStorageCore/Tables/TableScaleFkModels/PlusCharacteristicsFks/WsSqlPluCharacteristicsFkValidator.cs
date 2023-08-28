namespace WsStorageCore.Tables.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table validation "PLUS_CHARACTERISTICS_FK".
/// </summary>
public sealed class WsSqlPluCharacteristicsFkValidator : WsSqlTableValidator<WsSqlPluCharacteristicsFkModel>
{

    public WsSqlPluCharacteristicsFkValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Plu)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluValidator(isCheckIdentity));
        RuleFor(item => item.Characteristic)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlPluCharacteristicValidator(isCheckIdentity));
    }
}