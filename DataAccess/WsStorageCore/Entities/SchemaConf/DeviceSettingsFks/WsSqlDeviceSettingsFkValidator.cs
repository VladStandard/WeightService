using WsStorageCore.Entities.SchemaRef.Hosts;
namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

public sealed class WsSqlDeviceSettingsFkValidator : WsSqlTableValidator<WsSqlDeviceSettingsFkEntity>
{
    public WsSqlDeviceSettingsFkValidator(bool isCheckIdentity) : base(isCheckIdentity, false, false)
    {
        RuleFor(item => item.Host)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlHostValidator(isCheckIdentity));
        RuleFor(item => item.Setting)
            .NotEmpty()
            .NotNull()
            .SetValidator(new WsSqlDeviceSettingsValidator(isCheckIdentity));
    }
}