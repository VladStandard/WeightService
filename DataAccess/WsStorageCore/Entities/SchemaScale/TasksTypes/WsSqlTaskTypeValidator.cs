namespace WsStorageCore.Entities.SchemaScale.TasksTypes;

public sealed class WsSqlTaskTypeValidator : WsSqlTableValidator<WsSqlTaskTypeEntity>
{
    public WsSqlTaskTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
