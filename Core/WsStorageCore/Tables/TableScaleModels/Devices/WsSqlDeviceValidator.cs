namespace WsStorageCore.Tables.TableScaleModels.Devices;

/// <summary>
/// Table validation "DEVICES".
/// </summary>
public sealed class WsSqlDeviceValidator : WsSqlTableValidator<WsSqlDeviceModel>
{

    public WsSqlDeviceValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.LoginDt)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
        RuleFor(item => item.LogoutDt)
            .NotEmpty()
            .NotNull()
            .LessThanOrEqualTo(DateTime.Now.Date.AddDays(1));
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PrettyName)
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
        RuleFor(item => item.Ipv4)
           .NotNull();
        RuleFor(item => item.MacAddressValue)
            .NotNull();
    }
}
