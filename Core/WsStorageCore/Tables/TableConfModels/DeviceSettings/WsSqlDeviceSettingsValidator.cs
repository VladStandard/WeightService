namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

public sealed class WsSqlDeviceSettingsValidator : WsSqlTableValidator<WsSqlDeviceSettingsModel>
{
    public WsSqlDeviceSettingsValidator(bool isCheckIdentity) : base(isCheckIdentity, true, true)
    {
        RuleFor(item => item.Name)
            .NotEmpty()
            .NotNull();
    }
}