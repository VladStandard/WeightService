namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

public sealed class WsSqlDeviceSettingsFkValidator : WsSqlTableValidator<WsSqlDeviceSettingsFkEntity>
{
    public WsSqlDeviceSettingsFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Device)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceValidator(isCheckIdentity));
        RuleFor(item => item.Setting)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceSettingsValidator(isCheckIdentity));
    }
}