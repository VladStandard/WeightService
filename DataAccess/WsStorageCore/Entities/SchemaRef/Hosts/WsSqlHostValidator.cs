namespace WsStorageCore.Entities.SchemaRef.Hosts;

/// <summary>
/// Table validation "DEVICES".
/// </summary>
public sealed class WsSqlHostValidator : WsSqlTableValidator<WsSqlHostEntity>
{

    public WsSqlHostValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.Ip)
           .NotNull();
    }
}
