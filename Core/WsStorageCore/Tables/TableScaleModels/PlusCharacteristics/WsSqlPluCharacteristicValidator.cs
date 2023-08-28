namespace WsStorageCore.Tables.TableScaleModels.PlusCharacteristics;

public sealed class WsSqlPluCharacteristicValidator : WsSqlTableValidator<WsSqlPluCharacteristicModel>
{
    public WsSqlPluCharacteristicValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.AttachmentsCount)
            .NotNull()
            .GreaterThanOrEqualTo(0);
    }
}