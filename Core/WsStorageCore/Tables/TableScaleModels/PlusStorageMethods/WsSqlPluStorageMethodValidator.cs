namespace WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table validation "PLUS_STORAGE_METHODS".
/// </summary>
public sealed class WsSqlPluStorageMethodValidator : WsSqlTableValidator<WsSqlPluStorageMethodModel>
{

    public WsSqlPluStorageMethodValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.MinTemp)
            .NotNull()
            .LessThanOrEqualTo(item => item.MaxTemp);
        RuleFor(item => item.MaxTemp)
            .NotNull()
            .GreaterThanOrEqualTo(item => item.MinTemp);
    }
}