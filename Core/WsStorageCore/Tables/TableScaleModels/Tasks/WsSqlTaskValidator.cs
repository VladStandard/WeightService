namespace WsStorageCore.Tables.TableScaleModels.Tasks;

public sealed class WsSqlTaskValidator : WsSqlTableValidator<WsSqlTaskModel>
{
    public WsSqlTaskValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.TaskType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlTaskTypeValidator(isCheckIdentity));
        RuleFor(item => item.Scale)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlScaleValidator(isCheckIdentity));
    }
}
