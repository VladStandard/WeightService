// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.LogsWebsFks;

/// <summary>
/// Table validation "LOGS_WEBS_FK".
/// </summary>
public sealed class WsSqlLogWebFkValidator : WsSqlTableValidator<WsSqlLogWebFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="isCheckIdentity"></param>
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