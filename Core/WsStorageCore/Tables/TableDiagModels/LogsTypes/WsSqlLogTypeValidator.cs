namespace WsStorageCore.Tables.TableDiagModels.LogsTypes;

public sealed class WsSqlLogTypeValidator : WsSqlTableValidator<WsSqlLogTypeModel>
{
    public WsSqlLogTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Number)
            .NotNull()
            .GreaterThanOrEqualTo((byte)WsEnumLogType.None)
            .LessThanOrEqualTo((byte)WsEnumLogType.Information);
        RuleFor(item => item.Icon)
            .NotEmpty()
            .NotNull();
    }
}
