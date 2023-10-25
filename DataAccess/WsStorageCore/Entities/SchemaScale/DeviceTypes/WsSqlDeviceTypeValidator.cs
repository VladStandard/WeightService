namespace WsStorageCore.Entities.SchemaScale.DeviceTypes;

/// <summary>
/// Table validation "DEVICES_TYPES".
/// </summary>
public sealed class WsSqlDeviceTypeValidator : WsSqlTableValidator<WsSqlDeviceTypeModel>
{

    public WsSqlDeviceTypeValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.PrettyName)
            .NotEmpty()
            .NotNull();
        RuleFor(item => item.Description)
            .NotNull();
    }
}
