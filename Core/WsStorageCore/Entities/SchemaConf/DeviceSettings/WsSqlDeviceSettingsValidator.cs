namespace WsStorageCore.Entities.SchemaConf.DeviceSettings;

public sealed class WsSqlDeviceSettingsValidator : WsSqlTableValidator<WsSqlDeviceSettingsEntity>
{
    public WsSqlDeviceSettingsValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}