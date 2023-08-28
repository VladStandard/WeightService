namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

public sealed class WsSqlLogWebFkValidator : WsSqlTableValidator<WsSqlLogWebFkModel>
{
    public WsSqlLogWebFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.LogWebRequest)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlLogWebValidator(isCheckIdentity));
        RuleFor(item => item.LogWebResponse)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlLogWebValidator(isCheckIdentity));
        RuleFor(item => item.App)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlAppValidator(isCheckIdentity));
        RuleFor(item => item.LogType)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlLogTypeValidator(isCheckIdentity));
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
    }
}