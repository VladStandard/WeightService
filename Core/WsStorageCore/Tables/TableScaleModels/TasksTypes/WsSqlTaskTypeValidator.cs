namespace WsStorageCore.Tables.TableScaleModels.TasksTypes;

public sealed class WsSqlTaskTypeValidator : WsSqlTableValidator<WsSqlTaskTypeModel>
{
    public WsSqlTaskTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}
