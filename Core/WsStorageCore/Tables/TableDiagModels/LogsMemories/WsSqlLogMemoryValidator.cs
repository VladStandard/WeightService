namespace WsStorageCore.Tables.TableDiagModels.LogsMemories;

public sealed class WsSqlLogMemoryValidator : WsSqlTableValidator<WsSqlLogMemoryModel>
{
    public WsSqlLogMemoryValidator(bool isCheckIdentity) : base(isCheckIdentity, true, false)
    {
        RuleFor(item => item.SizeAppMb)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.SizeFreeMb)
            .NotNull()
            .GreaterThanOrEqualTo((short)0);
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlAppValidator(isCheckIdentity));
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
    }
}